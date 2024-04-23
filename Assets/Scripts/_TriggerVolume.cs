using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

[RequireComponent(typeof(BoxCollider))]
public class _TriggerVolume : MonoBehaviour
{
    public UnityEvent OnEnterTrigger;
    public UnityEvent OnExitTrigger;
    private Collider _playerCollider;
    public _DialogueInput _dialogue;

    public UnityEvent OnTriggerKeyDown;
    public KeyCode triggerKey;

    [Header("Gizmo Settings")] 
    [SerializeField] private bool _viewTriggerVolume = false;
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
            _DialogueManager dialogueManager = FindObjectOfType<_DialogueManager>();
            if (dialogueManager != null)
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
                _DialogueManager dialogueManager = FindObjectOfType<_DialogueManager>();
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
