using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollection : MonoBehaviour
{
    [SerializeField] List<Dialogue> dialogues;
    private int curDialogueIndex = 0;

    private void Awake()
    {
        if(dialogues.Count < 0)
        {
            Debug.LogError("No dialogues to play!");
            return;
        }

        GetComponent<DialogueTrigger>().dialogue = dialogues[curDialogueIndex];
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }


    public bool TryToPlayNextDialogue()
    {
        ++curDialogueIndex;
        if(curDialogueIndex >= dialogues.Count)
        {
            Debug.Log("Dialogue Session over");
            return false;
        }

        GetComponent<DialogueTrigger>().dialogue = dialogues[curDialogueIndex];
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        return true;
    }
}
