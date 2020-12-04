using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Action<Transform, bool> TransformFocusedAction { get; set; }
    bool isActivated { get; set; }
}

public interface IActionable
{
    void TriggerAction(Transform transformToFocus, bool value);
}
