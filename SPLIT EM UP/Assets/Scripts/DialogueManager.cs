using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// https://youtu.be/_nRzoTzeyxU
[System.Serializable]
public class DialogueBox
{
    public GameObject box;
    public TextMeshProUGUI text_box;
}

// Keep track of the current dialogue we are on!
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    [SerializeField] private int currentLine;  // How far am I in the story?

    [SerializeField] private DialogueBox curDialogueBox;
    [SerializeField] private GameObject continueButton;
    private bool isStoryFinished;

    private float textSpeed = 0.05f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (textSpeed > 0.01f)
                SetTextSpeed(textSpeed -= 0.01f);
            else if (textSpeed <= 0.0025f)
                SetTextSpeed(textSpeed -= 0.025f);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentLine = 0;
        isStoryFinished = false;
        sentences.Clear();  // Remove old story

        // Add each sentence to manager
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplaySentence();
    }

    public void DisplaySentence()
    {
        continueButton.SetActive(false);
        if (sentences.Count == 0)
        {
            EndDialogueSession();
            return;
        }
        currentLine += 1;
   
        string sentence = sentences.Dequeue();
        if (sentence.Length == 0)
        {
            return;
        }

        SetTextSpeed(0.05f);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        curDialogueBox.text_box.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            curDialogueBox.text_box.text += letter;
            yield return new WaitForSeconds(GetTextSpeed());  // Wait some time for the subsequent characters to be typed out!
        }

        continueButton.SetActive(true);
    }

    public void ClearDialogue()
    {
        curDialogueBox.text_box.text = "";
    }

    public int GetCurrentLine()
    {
        return currentLine;
    }

    public bool StoryFinished()
    {
        return isStoryFinished;
    }

    public Queue<string> GetSentences()
    {
        return sentences;
    }

    public void SetTextBox(string text)
    {
        curDialogueBox.text_box.text = text.ToString();
    }

    void SetTextSpeed(float speed)
    {
        textSpeed = speed;
    }

    float GetTextSpeed()
    {
        return textSpeed;
    }

    public void SetContinueActive(bool toggle)
    {
        continueButton.SetActive(toggle);
    }

    public void EndDialogueSession()
    {
        isStoryFinished = true;
        if (!FindObjectOfType<DialogueCollection>().TryToPlayNextDialogue())
        {
            curDialogueBox.box.SetActive(false);
        }
    }

}