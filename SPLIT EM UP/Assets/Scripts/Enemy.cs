using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 10f)]
    [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.player.transform.position, speed * Time.deltaTime);
    }
}
