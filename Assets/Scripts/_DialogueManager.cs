using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _DialogueManager : MonoBehaviour
{
    private Queue <string> sentences;
    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(_DialogueInput newDialogue)
    {
        Debug.Log("Started Interacting With" + newDialogue.name);

        sentences.Clear();

        foreach (string sentence in newDialogue._sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }
}
