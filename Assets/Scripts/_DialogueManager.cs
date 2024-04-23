using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _DialogueManager : MonoBehaviour
{
    public GameObject _nameTextObject;
    public GameObject _dialogueTextObject;
    public GameObject _promptTextObject;

    public int TextSpeed = 10;

    private Text _nameText;
    private Text _dialogueText;
    private Text _promptText;

    private Queue<string> sentences;

    private bool dialogueActive = false;

    private void Awake()
    {
        sentences = new Queue<string>();
        _nameText = _nameTextObject.GetComponent<Text>();
        _dialogueText = _dialogueTextObject.GetComponent<Text>();
        _promptText = _promptTextObject.GetComponent<Text>();

        if (_nameText == null)
        {
            Debug.LogError("Name text component not found!");
        }
        if (_dialogueText == null)
        {
            Debug.LogError("Dialogue text component not found!");
        }
        if (_promptText == null)
        {
            Debug.LogError("Propt Text component not found!");
        }
    }

    public void StartDialogue(_DialogueInput newDialogue)
    {
        Debug.Log("Started Interacting With" + newDialogue.name);

        dialogueActive = true;

        _promptText.text = newDialogue.prompt;

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
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        _dialogueText.text = "";
        float timeBetweenLetters = 1f / TextSpeed;
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    public bool HasNextSentence()
    {
        return sentences.Count > 0;
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    public void EndDialogue()
    {
        dialogueActive = false;
        Debug.Log("End of Conversation");
    }
}
