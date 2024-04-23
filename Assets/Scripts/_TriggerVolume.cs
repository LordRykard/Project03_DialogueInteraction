using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

[RequireComponent(typeof(BoxCollider))]
public class _TriggerVolume : MonoBehaviour
{
    [Header("Preview Settings")]
    [SerializeField] private bool _viewTriggerVolume = false;

    [Header("Dialogue Settings")]
    public _DialogueInput _dialogue;
    public _DialogueManager dialogueManager;


    [Header("Trigger Events")]
    public UnityEvent OnEnterTrigger;
    public UnityEvent OnExitTrigger;
    public KeyCode triggerKey;
    public UnityEvent OnTriggerKeyDown;

    //private settings
    private Collider _playerCollider;
    private bool isDialogueActive = false;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider>();
        _playerCollider.isTrigger = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            OnTriggerKeyDown.Invoke();
            if (dialogueManager != null && !dialogueManager.IsDialogueActive())
            {
                if (!dialogueManager.HasNextSentence())
                {
                    OnExitTrigger.Invoke();
                }
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
            OnEnterTrigger.Invoke();

            if (_dialogue != null && !isDialogueActive)
            {
                if (dialogueManager != null)
                {
                    dialogueManager.StartDialogue(_dialogue);
                    isDialogueActive = true;
                }
                else
                {
                    Debug.LogWarning("Dialogue manager not found!");
                }
            }
            else
            {
                Debug.LogWarning("Dialogue input not assigned!");
            }
    }

    public void OnTriggerExit(Collider other)
    {
        if (isDialogueActive)
        {
            OnExitTrigger.Invoke();
            isDialogueActive = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_viewTriggerVolume == false)
            return;
        if (_playerCollider == null)
        {
            _playerCollider = GetComponent<Collider>();
        }
        Gizmos.DrawCube(transform.position, _playerCollider.bounds.size);
    }
    private void OnDrawGizmosSelected()
    {
        if (_viewTriggerVolume == false)
            return;
    }
}
