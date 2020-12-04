using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static void DestroySubSlots()
    {
        SubSlot[] allSubSlots = FindObjectsOfType<SubSlot>();
        int length = allSubSlots.Length;
        for (int i = 0; i < length; i++)
        {
            Destroy(allSubSlots[i].gameObject);
        }
    }
}
