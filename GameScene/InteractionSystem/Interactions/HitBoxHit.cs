using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxHit : MonoBehaviour, IInteractable
{
    public Action<Transform, bool> TransformFocusedAction { get; set; }
    public bool isActivated { get; set; }

    void Start()
    {
        isActivated = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health) && isActivated)
            TransformFocusedAction?.Invoke(collision.gameObject.transform, true);
    }
}
