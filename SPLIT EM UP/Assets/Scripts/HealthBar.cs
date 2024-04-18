using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    float yOffset = -1.4f;
    private void Start()
    {
        SetHealth(GameManager.player.GetCurrentHealth());
    }

    public void SetHealth(float curValue)
    {
        healthText.text = "Health: " + curValue;
    }
}
