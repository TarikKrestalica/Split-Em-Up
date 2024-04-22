using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public enum PlayerState
{
    Default,
    HitByEnemy,
    HitEnemy
};

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

    [Header("Movement Animations")]
    [SerializeField] AnimationManager animationManager;

    [Header("Movement Constraints")]
    private float horInput;
    private float vertInput;
    [SerializeField] private float rotationSpeed;
    private bool locked = false;
    private int directional = 1;

    // Dead State
    float currentScore = 0f;
    float currentHealth = 100f;

    [Header("Fighting Area Logic")]
    float enemyHitDelay = 3.1f;
    float curEnemyHitDelay = 0f;
    private bool inFightingZone = false;
    [SerializeField] GameObject previousTarget;
    GameObject previousZonedArea;
    GameObject previousWave;
    PlayerState currentPlayerState;

    private int enemiesDefeated = 0;

    // Attack TimeFrame
    private float timeLapseAfterAttack = 1;
    private float curTimeLapse = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckForLockedMovement())
        {
            return;
        }

        RunMovement();
        RunAttackLogic(null);
        if(currentPlayerState == PlayerState.HitEnemy)
        {
            if(curTimeLapse < timeLapseAfterAttack)  // Attempt time delay to view attack before default
            {
                curTimeLapse += Time.deltaTime;
            }
            else
            {
                GameManager.ratCamera.PlayDefaultTextures();
                curTimeLapse = 0f;
            } 
        }
    }

    bool CheckForLockedMovement()
    {
        if (locked)
        {
            return true;
        }

        return false;
    }

    public void RunAttackLogic(GameObject target)
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (animationManager)
            {
                animationManager.PlayAnimation("Punch");
            }
  
            if (target != null)
            {
                GameManager.ratCamera.PlayHitEnemyTextures();
                AddEnemyDefeated();
                Destroy(target);
                currentScore += 10;
                GameManager.scoreManager.SetScore(currentScore);
                curEnemyHitDelay = 0f;
            }     
        }
    }

    void RunMovement()
    {
        // Movement Logic
        horInput = directional * Input.GetAxis("Horizontal");
        vertInput = directional * Input.GetAxis("Vertical");

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
            if(animationManager)
                animationManager.PlayAnimation("Walk");
        }
        else
        {
            if(animationManager)
                animationManager.PlayAnimation("Idle");
        }

 
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.gameObject.tag == "Boss Zone")
        {
            horInput = directional * Input.GetAxis("Vertical");
            vertInput = directional * Input.GetAxis("Horizontal");
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
        if(other.gameObject.tag == "CameraStop")
        {
            GameObject target = other.gameObject;
            previousTarget = target;
            target.transform.GetChild(0).gameObject.SetActive(true);
            GameObject zoned = target.transform.GetChild(0).gameObject;
            previousZonedArea = zoned;
            target.transform.GetChild(1).gameObject.SetActive(true);
            previousWave = target.transform.GetChild(1).gameObject;

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Normal")
        {
            previousWave.SetActive(false);
            previousWave = null;
            previousZonedArea.SetActive(true);
            previousZonedArea = null;
        }
    }

    // Enemy hitting logic!
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy component = collision.gameObject.GetComponent<Enemy>();
            if (!component)
            {
                Debug.Log("Component can not be found!");
                return;
            }

            GameManager.ratCamera.PlayHitByEnemyTextures();
            if (curEnemyHitDelay <= 0)
            {
                currentHealth -= 10;
                GameManager.healthBar.SetHealth(currentHealth);
                curEnemyHitDelay = enemyHitDelay;
            }


            curEnemyHitDelay -= Time.deltaTime;        
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool AtFightingZone()
    {
        return inFightingZone;
    }

    public void SetAtFightingZone(bool toggle)
    {
        inFightingZone = toggle;
    }

    public PlayerState GetPlayerState()
    {
        return currentPlayerState;
    }

    public void SetPlayerState(PlayerState state)
    {
        currentPlayerState = state;
    }

    public GameObject GetTargetZone()
    {
        return previousZonedArea;
    }

    public void SetMovementLocked(bool toggle)
    {
        locked = toggle;
    }

    public bool IsMovementLocked()
    {
        return locked;
    }

    public void SetDirectional(int value)
    {
        directional = value;
    }

    public void AddEnemyDefeated()
    {
        enemiesDefeated += 1;
    }

    public void ResetEnemiesDefeated()
    {
        enemiesDefeated = 0;
    }

    public int GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }
}
