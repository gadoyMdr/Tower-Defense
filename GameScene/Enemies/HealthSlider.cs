using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    private Health health;


    private void Awake()
    {
        health = transform.root.GetComponent<Health>();
    }

    private void OnEnable() => health.HealthUpdatedAction += UpdateSlider;

    private void OnDisable() => health.HealthUpdatedAction -= UpdateSlider;

    private void UpdateSlider(float currentHealth, float startHealth)
    {
        healthSlider.value = currentHealth / startHealth;
        if (healthSlider.value == 0) Destroy(gameObject);
    }
}
