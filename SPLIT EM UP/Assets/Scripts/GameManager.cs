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

    public static RatCameraManager ratCamera
    {
        get
        {
            if (gameManager.m_ratCamera == null)
            {
                gameManager.m_ratCamera = GameObject.FindGameObjectWithTag("RatCamera").GetComponent<RatCameraManager>();
            }

            return gameManager.m_ratCamera;
        }
    }

    private RatCameraManager m_ratCamera;

    public static BossZone bossZone
    {
        get
        {
            if (gameManager.m_bossZone == null)
            {
                gameManager.m_bossZone = GameObject.FindGameObjectWithTag("BossZone").GetComponent<BossZone>();
            }

            return gameManager.m_bossZone;
        }
    }

    private BossZone m_bossZone;

    public static EmptyManager emptyManager
    {
        get
        {
            if (gameManager.m_emptyManager == null)
            {
                gameManager.m_emptyManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EmptyManager>();
            }

            return gameManager.m_emptyManager;
        }
    }

    private EmptyManager m_emptyManager;

    public static FinalBoss finalBoss
    {
        get
        {
            if (gameManager.m_finalBoss == null)
            {
                gameManager.m_finalBoss = GameObject.FindGameObjectWithTag("FinalBoss").GetComponent<FinalBoss>();
            }

            return gameManager.m_finalBoss;
        }
    }

    private FinalBoss m_finalBoss;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
    }

}
