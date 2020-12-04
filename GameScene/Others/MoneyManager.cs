using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public Action<int, int> MoneyChanged;

    private int Money;
    public int money
    {
        get => Money;
        set
        {
            MoneyChanged?.Invoke(value, value - Money);
            Money = value;
        }
    }

    private Level levelData;

    private void Awake()
    {
        
    }

    private void Start()
    {
        levelData = FindObjectOfType<Level>();
        money = levelData.startMoney;
    }
}
