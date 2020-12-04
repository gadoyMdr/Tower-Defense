using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    LineRenderer _lineRenderer;
    Tower tower;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        tower = GetComponent<Tower>();
    }

    private void OnEnable() => tower.FocusNewTarget += (x, y) => { startPoint = x; endPoint = y; };

    private void Update()
    {
        ShowLaser();
    }

    void ShowLaser()
    {
        _lineRenderer.SetPosition(0, startPoint);
        _lineRenderer.SetPosition(1, endPoint);
    }
}
