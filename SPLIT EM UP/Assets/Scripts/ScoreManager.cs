using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    float currentScore = 0f;

    private void Awake()
    {
        SetScore(currentScore);
    }

    public void SetScore(float curScore)
    {
        currentScore = curScore;
        scoreText.text = "Score : " + currentScore;
    }
}
