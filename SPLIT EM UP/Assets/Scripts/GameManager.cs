using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public static Player player
    {
        get
        {
            if(gameManager.m_player == null)
            {
                gameManager.m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return gameManager.m_player;
        }
    }

    private Player m_player;

    public static ScoreManager scoreManager
    {
        get
        {
            if (gameManager.m_scoreManager == null)
            {
                gameManager.m_scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
            }

            return gameManager.m_scoreManager;
        }
    }

    private ScoreManager m_scoreManager;

    public static HealthBar healthBar
    {
        get
        {
            if (gameManager.m_healthBar == null)
            {
                gameManager.m_healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
            }

            return gameManager.m_healthBar;
        }
    }

    private HealthBar m_healthBar;


    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
    }

}
