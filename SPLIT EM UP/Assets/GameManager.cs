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
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
    }

}
