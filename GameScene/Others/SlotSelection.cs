using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSelection : MonoBehaviour
{
    public Action<int> HotBarSelection;

    void Update()
    {
        CheckHotBarInputs();
    }

    void CheckHotBarInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            HotBarSelection?.Invoke(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            HotBarSelection?.Invoke(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            HotBarSelection?.Invoke(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            HotBarSelection?.Invoke(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            HotBarSelection?.Invoke(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            HotBarSelection?.Invoke(5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            HotBarSelection?.Invoke(6);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            HotBarSelection?.Invoke(7);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            HotBarSelection?.Invoke(8);
    }
}
