using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool isControlled = true;

    [SerializeField]
    [Range(0.1f, 2)]
    private float speed = 1;

    [SerializeField]
    [Range(1.5f, 10)]
    private float speedMultiWhenShift = 2;

    [SerializeField]
    [Range(2f, 5)]
    private float zoomSteps = 3;

    [SerializeField]
    [Range(1f, 20)]
    private float speedAngle = 5;

    public float yaw = 0;
    public float pitch = 0;

    private float tempSpeedAngle = 0f;
    private const float maxCameraZoom = 60;
    private const float minCameraZoom = 10;

    private void Start()
    {
        yaw = transform.eulerAngles.x;
        pitch = transform.eulerAngles.y;
    }

    void Update()
    {
        tempSpeedAngle = Camera.main.fieldOfView / maxCameraZoom;

        if (isControlled)
        {
            CheckMovementInputs();
            CheckZoomInput();
            CheckCameraAngles();
        }
        
    }


    void CheckCameraAngles()
    {
        if (Input.GetMouseButton(1))
        {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            transform.Rotate(transform.up, tempSpeedAngle * Input.GetAxis("Mouse X"), Space.World);
            transform.Rotate(transform.right, -(tempSpeedAngle * Input.GetAxis("Mouse Y")), Space.World);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

        }
        
        if(Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    void CheckZoomInput()
    {
        if (Input.GetKey(KeyCode.LeftControl)) return ;

        float finalZoomLevel = Camera.main.fieldOfView;

        if (Input.mouseScrollDelta.y < 0)
            finalZoomLevel += zoomSteps;
        if (Input.mouseScrollDelta.y > 0)
            finalZoomLevel -= zoomSteps;

        finalZoomLevel = Mathf.Clamp(finalZoomLevel, minCameraZoom, maxCameraZoom);

        Camera.main.fieldOfView = finalZoomLevel;
    }

    void CheckMovementInputs()
    {
        Vector3 finalDirectionVector = Vector3.zero;

        if (Input.GetKey(KeyCode.Z))
            finalDirectionVector += transform.forward;
        if (Input.GetKey(KeyCode.Q))
            finalDirectionVector += -transform.right;
        if (Input.GetKey(KeyCode.S))
            finalDirectionVector += -transform.forward;
        if (Input.GetKey(KeyCode.D))
            finalDirectionVector += transform.right;
        if (Input.GetKey(KeyCode.A))
            finalDirectionVector += Vector3.up;
        if (Input.GetKey(KeyCode.W))
            finalDirectionVector += Vector3.down;

        finalDirectionVector = finalDirectionVector.normalized * speed * tempSpeedAngle;

        if (Input.GetKey(KeyCode.LeftShift))
            finalDirectionVector *= speedMultiWhenShift;

        transform.position = transform.position + finalDirectionVector;

    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
