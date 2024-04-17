using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class _TriggerVolume : MonoBehaviour
{
    public UnityEvent OnEnterTrigger;
    private Collider _playerCollider;
    public _DialogueInput _dialogue;

    public UnityEvent OnTriggerKeyDown;
    public KeyCode triggerKey;

    [Header("Gizmo Settings")] [SerializeField] private bool _viewTriggerVolume = false;

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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnterTrigger.Invoke();
        FindObjectOfType<_DialogueManager>().StartDialogue(_dialogue);
    }
    private void OnDrawGizmos()
    {
        if (_viewTriggerVolume == false)
            return;
        if(_playerCollider == null)
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
