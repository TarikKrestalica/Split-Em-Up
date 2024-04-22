using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Handling all boss battle events here!
public class BossZone : FightingZone
{
    [SerializeField] DialogueCollection collection;
    private bool hasReversedControls = false;

    public override void OnTriggerEnter(Collider other)
    {
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
        hasReversedControls = true;
    }
}
