using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float blinkSpeed = 2f;

    private TextMeshProUGUI text;

    private float currentBlinkValue = 0;
    private float target;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Blink();
        UpdateTransparency();
    }

    void UpdateTransparency()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, currentBlinkValue);
    }

    void Blink()
    {
        if (currentBlinkValue <= 0.1f)
            target = 1;
        if (currentBlinkValue >= 0.9f)
            target = 0;
        
        currentBlinkValue = Mathf.Lerp(currentBlinkValue, target, blinkSpeed * Time.deltaTime);
    }
}
