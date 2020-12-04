using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LandObstacle : MyObjectToPlace
{
    public override bool CanBePlaced()
    {
        return _collidersInTrigger.colliders.Any(x => x.gameObject.name.Contains("Path"));
    }
}
