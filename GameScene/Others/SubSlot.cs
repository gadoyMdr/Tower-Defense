using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubSlot : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;

    [HideInInspector]
    public MyObjectToPlace currentObject;

    [HideInInspector]
    public VerticalSubMenu verticalSubMenu;

    public void OnClick()
    {
        verticalSubMenu.Select(currentObject);
    }

    public void SetObjectAndParent(MyObjectToPlace objectToPlace, VerticalSubMenu verticalSubMenu)
    {
        currentObject = objectToPlace;
        this.verticalSubMenu = verticalSubMenu;

        itemImage.sprite = objectToPlace.sprite;
        priceText.text = $"Price : {objectToPlace.price} $";
        nameText.text = objectToPlace.name;
    }
}
