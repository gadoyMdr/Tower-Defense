using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldRaycast : MonoBehaviour
{
    [HideInInspector]
    public Vector3 point = Vector3.zero;

    [HideInInspector]
    public GameObject gameObjectHit = null;

    [HideInInspector]
    public bool isMouseOverUI;

    [HideInInspector]
    public List<RaycastResult> allUIs = new List<RaycastResult>();

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    private void Awake()
    {
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        ThrowRaycast();
        CheckIfMouseHoverUI();
    }

    void ThrowRaycast()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            point = hit.point;
            gameObjectHit = hit.collider.gameObject;
        }
            
        else
        {
            point = Vector3.one * 653416815321;
            gameObjectHit = null;
        }
            
    }

    void CheckIfMouseHoverUI()
    {
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        allUIs = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, allUIs);

        isMouseOverUI = allUIs.Count > 0;
    }
}
