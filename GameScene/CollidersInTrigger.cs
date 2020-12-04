using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersInTrigger : MonoBehaviour
{
    public List<Collider> colliders = new List<Collider>();

    public void OnTriggerEnter(Collider other) => colliders.Add(other);
    

    public void OnTriggerExit(Collider other) => colliders.Remove(other);
    

}
