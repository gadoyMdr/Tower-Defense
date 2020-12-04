using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerBrain : MonoBehaviour, IActionable
{
    private Tower tower;

    [HideInInspector]
    public bool isControlledByAi = true;

    private void Awake()
    {
        tower = GetComponent<Tower>();
    }

    public void TriggerAction(Transform transformToFocus, bool value)
    {
        if (isControlledByAi && value)
        {
            LookAt(transformToFocus);
            FireAt();
        }
        
    }

    void LookAt(Transform transformToFocus)
    {
        tower.GetAverageMuzzlePoint();

        //Otherwise, the barrel will aim at the feet of the enemy, we want the center
        Vector3 correctedPos = new Vector3(transformToFocus.position.x, transformToFocus.position.y + 1, transformToFocus.position.z);

        tower.gunRotate.rotation = Quaternion.LookRotation(correctedPos - tower.averageMuzzlePointsPos);
    }

    void FireAt()
    {
        tower.TryFire();
    }
}
