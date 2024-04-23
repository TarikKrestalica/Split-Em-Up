using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

// Handling all boss battle events here!
public class BossZone : FightingZone
{
    [SerializeField] DialogueCollection collection;
    private bool hasReversedControls = false;
    private GameObject finalBoss;
    [SerializeField] GameObject boss;
    [Range(0, 10)]
    [SerializeField] private float bossSpeed;

    private void Start()
    {
        finalBoss = boss;
        finalBoss.SetActive(false);
    }

    private void Update()
    {
        if (!hasReversedControls)
            return;

    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            GameManager.player.SetAtFightingZone(true);
            SetBarriersActive(true);
            GameManager.player.SetMovementLocked(true);
            collection.TryToPlayNextDialogue();
        }

    }

    public void ReverseMovementControls()
    {
        int newDirectional = -1;
        GameManager.player.SetDirectional(newDirectional);
    }

    public void StartBossFight()
    {
        ReverseMovementControls();
        GameManager.player.SetMovementLocked(false);
        finalBoss.SetActive(true);
        hasReversedControls = true;
    }
}
