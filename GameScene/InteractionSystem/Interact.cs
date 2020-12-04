using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IActionable))]
[RequireComponent(typeof(IInteractable))]
public class Interact : MonoBehaviour
{
    private IActionable action;

    private IInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
        action = GetComponent<IActionable>();
    }

    private void OnEnable()
    {
        interactable.TransformFocusedAction += action.TriggerAction;
    }

    private void OnDisable()
    {
        interactable.TransformFocusedAction -= action.TriggerAction;
    }
}
