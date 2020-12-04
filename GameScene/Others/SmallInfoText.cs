using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmallInfoText : MonoBehaviour
{
    TextMeshProUGUI text;

    WaveManager waveManager;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {
        ActivateText(false);
    }

    private void OnEnable()
    {
        waveManager.WaveInfo += UpdateText;
        waveManager.ActivateInfoText += ActivateText;
    }

    private void OnDisable()
    {
        waveManager.WaveInfo -= UpdateText;
        waveManager.ActivateInfoText -= ActivateText;
    }

    void UpdateText(string value, Color color)
    {
        text.text = value;
        text.color = color;
    }

    void ActivateText(bool value)
    {
        text.enabled = value;
    }
}
