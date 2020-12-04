using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Range(0, 100)]
    public float rotationSpeed = 30;

    [SerializeField]
    Transform transformToRotate;

    [SerializeField]
    private bool worldSpace = true;

    [SerializeField]
    bool XAxis = true;

    [SerializeField]
    bool YAxis = true;

    [SerializeField]
    bool ZAxis = true;


    private Transform finalTransform;

    private void Start()
    {
        finalTransform = transformToRotate == null ? transform : transformToRotate;
    }

    void Update()
    {
        RotateMethod();
    }

    private void RotateMethod()
    {
        Vector3 finalRotation = Vector3.zero;

        if (XAxis)
            finalRotation += new Vector3(rotationSpeed * Time.deltaTime, 0, 0);
        if (YAxis)
            finalRotation += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        if (ZAxis)
            finalRotation += new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        if (worldSpace)
            finalTransform.Rotate(finalRotation, Space.World);
        else
            finalTransform.Rotate(finalRotation, Space.Self);
    }
}
