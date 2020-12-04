using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPopup : MonoBehaviour
{
    TextMeshProUGUI text;

    float transparency = 1;

    const float transparencyFadeOut = 0.05f;
    const float movingSpeed = 30f;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(ColorGradiant());
    }

    public void SetParameters(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;
    }

    private void Update()
    {
        
        CheckColor();
        UpdatePositionAndColor();
    }

    void UpdatePositionAndColor()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, transparency);
        gameObject.transform.position += Vector3.up * Time.deltaTime * movingSpeed;
    }

    void CheckColor()
    {
        if (text.color.a <= 0) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator ColorGradiant()
    {
        yield return new WaitForSeconds(0.1f);
        transparency -= transparencyFadeOut;
        StartCoroutine(ColorGradiant());
    }
}
