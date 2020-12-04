using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFaceTransform : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private Camera cameraToFace;

    [SerializeField]
    private bool invert = false;

    private Camera actualCameraToFace;
    private RectTransform actualRectTransformToRotate;

    private void Awake()
    {
        actualCameraToFace = cameraToFace == null ? Camera.main : cameraToFace;
        actualRectTransformToRotate = rectTransform == null ? GetComponent<RectTransform>() : rectTransform;
    }

    private void Update()
    {
        FaceCamera();
    }

    void FaceCamera()
    {
        actualRectTransformToRotate.transform.forward = actualCameraToFace.transform.forward;
        if (invert)
            actualRectTransformToRotate.transform.forward = -actualRectTransformToRotate.transform.forward;
    }
}
