using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 10f)]
    [SerializeField] protected float speed;

    private bool nearPlayer = false;

    [SerializeField] Animator animator;
    [SerializeField] AnimationClip clip;

    public virtual void Update()
    {
        animator.Play(clip.name);
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.player.transform.position, speed * Time.deltaTime);
        Vector3 distanceApart = GameManager.player.transform.position - this.transform.position;
        if (distanceApart.magnitude < 5)
        {
            nearPlayer = true;
            GameManager.player.RunAttackLogic(this.gameObject);
        }
        else
        {
            nearPlayer = false;
        }
    }

    public bool NearPlayer()
    {
        return nearPlayer;
    }
}
