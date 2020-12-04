using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InitiateLevel : MonoBehaviour
{
    public Action LevelReady;

    private void Awake()
    {
        Instantiate(LoadMap.map, null);
        LevelReady?.Invoke();
    }
}
