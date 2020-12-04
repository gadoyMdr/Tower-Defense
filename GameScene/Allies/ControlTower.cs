using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(TowerBrain))]
public class ControlTower : MonoBehaviour
{
    [SerializeField]
    private Transform playerViewPoint;

    [SerializeField]
    private float rotSpeed;

    private bool IsControlled;
    public bool isControlled
    {
        get => IsControlled;
        set
        {
            IsControlled = value;
            SetCameraToView(value);
            CameraController.isControlled = !value;
            _towerBrain.isControlledByAi = !value;
        }
    }

    private Tower _tower;
    private TowerBrain  _towerBrain;

    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerBrain = GetComponent<TowerBrain>();
    }

    private void Start()
    {
        _towerBrain.isControlledByAi = true;
    }

    void Update()
    {
        if (isControlled)
            Control();
    }

    void Control()
    {
        CheckFire();
        CheckMouseRotation();
    }

    void CheckFire()
    {
        if (Input.GetMouseButton(0))
            _tower.TryFire();
    }

    void CheckMouseRotation()
    {
        _tower.gunRotate.Rotate(_tower.gunRotate.up, rotSpeed * Input.GetAxis("Mouse X"), Space.World);
        _tower.gunRotate.Rotate(_tower.gunRotate.right, -(rotSpeed * Input.GetAxis("Mouse Y")), Space.World);
        _tower.gunRotate.localEulerAngles = new Vector3(_tower.gunRotate.localEulerAngles.x, _tower.gunRotate.localEulerAngles.y, 0);
    }

    void SetCameraToView(bool value)
    {
        if (value)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Camera.main.transform.SetParent(playerViewPoint);
            Camera.main.transform.position = playerViewPoint.position;
            Camera.main.transform.rotation = playerViewPoint.rotation;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Camera.main.transform.SetParent(null);

            Camera.main.transform.position = Camera.main.transform.position + Vector3.up * 2;
        }
        
    }

    public void TakeControl() => isControlled = true;
}
