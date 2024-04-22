using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollection : MonoBehaviour
{
    [SerializeField] List<Dialogue> dialogues;
    private int curDialogueIndex = -1;

    public bool TryToPlayNextDialogue()
    {
        if (dialogues.Count < 0)
        {
            Debug.LogError("No dialogues to play!");
            return false;
        }

        ++curDialogueIndex;
        if(curDialogueIndex >= dialogues.Count)
        {
            GameManager.player.SetMovementLocked(false);
            return false;
        }

        GetComponent<DialogueTrigger>().dialogue = dialogues[curDialogueIndex];
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        return true;
    }
}
