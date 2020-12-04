using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanBeSelected))]
public class ShowSmallCanvasWhenSelected : MonoBehaviour
{
    [SerializeField]
    private GameObject smallCanvas;

    private CanBeSelected canBeSelected;

    private void Awake()
    {
        canBeSelected = GetComponent<CanBeSelected>();
    }

    private void Start()
    {
        ShowCanvas(false);
        smallCanvas.transform.SetParent(GameObject.FindGameObjectWithTag("GameCanvas").transform);
    }

    private void OnEnable() => canBeSelected.SelectedAction += ShowCanvas;

    private void OnDisable() => canBeSelected.SelectedAction -= ShowCanvas;

    private void Update()
    {
        UpdateCanvasPos();
    }

    void UpdateCanvasPos()
    {
        smallCanvas.transform.position = 
            Camera.main.WorldToScreenPoint
            (
                new Vector3(
                    transform.position.x,
                    transform.position.y + GetComponent<BoxCollider>().size.y,
                    transform.position.z
                    )
            );
    }

    private void OnDestroy()
    {
        Destroy(smallCanvas.gameObject);
    }

    void ShowCanvas(bool value)
    {
        smallCanvas.SetActive(value); 
    }

}
