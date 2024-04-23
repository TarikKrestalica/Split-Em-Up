using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : Enemy
{
    private float health = 100;
    private HealthBar healthBar;

    private void Start()
    {
        if (!GetHealthComponent())
            return;

        healthBar.SetHealth(health);
    }

    bool GetHealthComponent()
    {
        healthBar = GetComponent<HealthBar>();
        if (!healthBar)
        {
            Debug.Log("No health bar");
            return false;
        }

        return true;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("WinScreen");
            return;
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.player.transform.position, speed * Time.deltaTime);
        Vector3 distanceApart = GameManager.player.transform.position - this.transform.position;
        if (distanceApart.magnitude < 3)
        {
            GameManager.player.RunAttackLogic(this.gameObject);
        }
    }

    public void TakeDamage()
    {
        health -= 10;
        if (!GetHealthComponent())
            return;

        healthBar.SetHealth(health);
    }
}
