using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRedScreen : MonoBehaviour, IActionable
{
    [SerializeField]
    private Canvas canvasPrefab;

    private Canvas canvasInstance;
    public void TriggerAction(Transform transformToFocus, bool value)
    {
        if (value && canvasInstance == null)
            canvasInstance = Instantiate(canvasPrefab);
        else if (!value && canvasInstance != null)
            Destroy(canvasInstance.gameObject);
    }

    
}
