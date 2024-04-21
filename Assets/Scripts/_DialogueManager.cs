using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _DialogueManager : MonoBehaviour
{
    public GameObject _nameTextObject;
    public GameObject _dialogueTextObject;

    private Text _nameText;
    private Text _dialogueText;

    private Queue <string> sentences;
    private void Awake()
    {
        sentences = new Queue<string>();
        _nameText = _nameTextObject.GetComponent<Text>();
        _dialogueText = _dialogueTextObject.GetComponent<Text>();

        if (_nameText == null)
        {
            Debug.LogError("Name text component not found!");
        }
        if (_dialogueText == null)
        {
            Debug.LogError("Dialogue text component not found!");
        }
    }
    public void StartDialogue(_DialogueInput newDialogue)
    {
        Debug.Log("Started Interacting With" + newDialogue.name);

        _nameText.text = newDialogue.name;

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
        _dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }
}
