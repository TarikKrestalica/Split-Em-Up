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
            SetBarriersActive(true);
            collection.TryToPlayNextDialogue();
            GameManager.player.SetMovementLocked(true);
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
