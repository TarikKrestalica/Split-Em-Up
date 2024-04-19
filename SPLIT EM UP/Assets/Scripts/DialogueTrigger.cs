using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/_nRzoTzeyxU

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
