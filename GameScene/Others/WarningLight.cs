using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class WarningLight : MonoBehaviour
{
    Light _light;

    WaveManager waveManager;

    private void Awake()
    {
        _light = GetComponent<Light>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {
        TurnLight(false);
        Randomize();
    }

    private void OnEnable() => waveManager.EnemyBeingSpawnedUpdate += TurnLight;

    private void OnDisable() => waveManager.EnemyBeingSpawnedUpdate -= TurnLight;

    void Randomize()
    {
        Rotate currentLight = GetComponent<Rotate>();
        currentLight.rotationSpeed *= Random.Range(0.75f, 1.25f);
    }

    void TurnLight(bool value)
    {
        StartCoroutine(TurnLightDelay(value));
    }

    IEnumerator TurnLightDelay(bool value)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        _light.enabled = value;
    }
}
