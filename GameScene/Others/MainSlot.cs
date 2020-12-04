using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainSlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private VerticalSubMenu verticalSubMenu;

    [SerializeField]
    private Transform visuals;

    public Image itemImage;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;

    [HideInInspector]
    public MyObjectToPlace currentObject;

    public int ID;

    private SlotSelection slotSelection;

    private void Awake()
    {
        slotSelection = FindObjectOfType<SlotSelection>();
    }

    private void Start()
    {
        verticalSubMenu.parentMainSlot = this;
    }

    public void SetObjects(List<MyObjectToPlace> objectsOfType)
    {
        verticalSubMenu.objectList = objectsOfType;
    }

    public void SetParameters(int ID)
    {
        this.ID = ID;
        PickRandomSubSlot();
    }

    void PickRandomSubSlot()
    {
        SetObject(verticalSubMenu.objectList[Random.Range(0, verticalSubMenu.objectList.Count)]);
    }

    public void SetObject(MyObjectToPlace objectToPlace)
    {
        currentObject = objectToPlace;

        itemImage.sprite = objectToPlace.sprite;
        priceText.text = $"Price : {objectToPlace.price} $";
        nameText.text = objectToPlace.name;

        slotSelection.HotBarSelection?.Invoke(ID);
    }

    public void OnClick()
    {
        slotSelection.HotBarSelection?.Invoke(ID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        verticalSubMenu.Unfold(currentObject);       
    }

    public void HideVisuals(bool value)
    {
        visuals.gameObject.SetActive(value);
    }
}
