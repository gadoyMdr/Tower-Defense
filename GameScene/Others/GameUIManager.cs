using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Money")]
    [SerializeField]
    private TextPopup textPopupPrefab;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [Header("Wave Progression")]
    [SerializeField]
    private Slider waveProgressionSlider;

    [SerializeField]
    private TextMeshProUGUI enemyHealth;

    [SerializeField]
    private TextMeshProUGUI outOf;

    [SerializeField]
    private GameObject bigText;


    [SerializeField]
    private Image fillSliderImage;

    WaveManager waveManager;
    MoneyManager moneyManager;
    Death death;
    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        
    }

    private void Start()
    {
        UpdateBigText("", false);

        death = FindObjectOfType<Death>();

        waveManager.EnemiesHealthUpdated += UpdateSlider;
        waveManager.BigText += UpdateBigText;
        moneyManager.MoneyChanged += UpdateMoney;
        death.BigText += UpdateBigText;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        waveManager.EnemiesHealthUpdated -= UpdateSlider;
        waveManager.BigText -= UpdateBigText;
        moneyManager.MoneyChanged -= UpdateMoney;
        death.BigText -= UpdateBigText;
    }

    void UpdateMoney(int value, int difference)
    {
        moneyText.text = $"Money : {value} $";

        
        TextPopup instance = Instantiate(textPopupPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("GameCanvas").transform.Find("BottomLeft"));

        instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-38, 20);

        if (difference > 0) instance.SetParameters($"+ {difference} $", Color.green);
        else instance.SetParameters($"{difference} $", Color.red);
    }

    void UpdateSlider(float currentHealth, float startHealth)
    {
        enemyHealth.text = $"Enemy health : {currentHealth:F0}";
        outOf.text = $"Out of : {startHealth}";

        fillSliderImage.color = Color.Lerp(Color.green, Color.red, currentHealth / startHealth);
        waveProgressionSlider.value = currentHealth / startHealth;
    }

    void UpdateBigText(string text, bool value)
    {
        bigText.SetActive(value);
        bigText.transform.Find("BigText").GetComponent<TextMeshProUGUI>().text = text;
    }
}
