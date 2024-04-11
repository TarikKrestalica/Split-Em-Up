using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [Range(1, 20)]
    [SerializeField] private float speed;
    [Range(0, .9f)]
    [SerializeField] private float friction;
    private Rigidbody rb;
    [SerializeField] private RectTransform groundCheckTransform;
    [SerializeField] private LayerMask groundMask;

    [Header("Movement Constraints")]
    private float horInput;
    private float vertInput;
    [SerializeField] private float rotationSpeed;

    [Header("Jumping")]
    [Range(1, 10)]
    [SerializeField] private float jumpStrength;
    [Range(0.1f, .9f)]
    [SerializeField] private float jumpMovementDecay;

    private Vector3 startingPosition;

    // Dead State
    private bool isDead = false;
    private bool isAtGoal = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
        startingPosition = this.transform.position;
    }

    void GetComponents()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RunMovement();
        RunJump();
    }

    void RunMovement()
    {
        // Movement Logic
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        if (!IsGrounded())
        {
            horInput /= jumpMovementDecay;
            vertInput /= jumpMovementDecay;
        }

        // Fixing player rotation: https://youtu.be/v6Kh748AwJU?si=-k-4hQngw7fizIyU
        Vector3 myVel = new Vector3(horInput, 0, vertInput);
        if (myVel.magnitude > 1)
        {
            myVel = myVel.normalized;
        }

        // Change the movement logic to accomodate for rotation 
        transform.Translate(myVel * speed * Time.deltaTime, Space.World);
        if (myVel != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(myVel, Vector3.up);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void RunJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsGrounded())
            {
                return;
            }
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }

    void CapVelocity()  // Give threshold for the speed!
    {
        if (Mathf.Abs(rb.velocity.x) > speed)
        {
            rb.velocity =
                rb.velocity.x > 0 ?
                new Vector3(speed, rb.velocity.y, rb.velocity.z) :
                new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }
        else if (Mathf.Abs(rb.velocity.z) > speed)
        {
            rb.velocity =
                rb.velocity.z > 0 ?
                new Vector3(rb.velocity.x, rb.velocity.y, speed) :
                new Vector3(rb.velocity.x, rb.velocity.y, -speed);
        }
    }

    // Controls player movement tightness
    void Friction()
    {
        if (rb.velocity.y != 0)
        {
            return;
        }

        Vector3 vel = rb.velocity;
        vel.x *= friction;
        rb.velocity = vel;
    }

    private void FixedUpdate()
    {
        Friction();
        CapVelocity();
    }

    // Had right idea, but needed help with the syntax: https://docs.unity3d.com/ScriptReference/Physics.CheckCapsule.html
    // Checks if the feet of the player is collided with the ground
    public bool IsGrounded()
    {
        return Physics.CheckCapsule(groundCheckTransform.position, groundCheckTransform.position, 0.2f, groundMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillFloor")
        {
            SetDeadState(true);
        }
    }

    // Death Logic
    public bool IsDead()
    {
        return isDead;
    }

    public void SetDeadState(bool toggle)
    {
        isDead = toggle;
    }

    // Goal Logic
    public bool IsAtGoal()
    {
        return isAtGoal;
    }

    public void SetAtGoalState(bool toggle)
    {
        isAtGoal = toggle;
    }

    public Vector3 GetStartingPosition()
    {
        return startingPosition;
    }
}
