using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Action DoneLoading;

    public int startMoney = 800;
    public List<Transform> waypoints = new List<Transform>();
    public List<WaveData> waves = new List<WaveData>();
    public Transform spawnPoint;

    private void Start()
    {
        DoneLoading?.Invoke();
    }
}
