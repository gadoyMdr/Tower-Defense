using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action<float, float> HealthUpdatedAction;
    public Action DieEvent;

    [Range(1,400)]
    public float startHealth;

    private float CurrentHealth;
    public float currentHealth
    {
        get => CurrentHealth;
        set
        {
            CurrentHealth = value;

            if (currentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }

            HealthUpdatedAction?.Invoke(value, startHealth);
        }
    }

    private void Start()
    {
        currentHealth = startHealth;
    }

    void Die()
    {
        GiveMoney();
        if (DieEvent == null)
            Destroy(gameObject);
        else
            DieEvent.Invoke();

    }

    void GiveMoney()
    {
        FindObjectOfType<MoneyManager>().money += (int) (startHealth / 4);
    }
}
