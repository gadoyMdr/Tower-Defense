using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CollidersInTrigger))]
[RequireComponent(typeof(CanBeSelected))]
public abstract class MyObjectToPlace : MonoBehaviour
{
    [SerializeField]
    private Transform visuals;

    public Sprite sprite;

    public new string name;

    public int price = 50;

    public int sellPrice = 38;

    public AllyType allyType = AllyType.Tower;

    [HideInInspector]
    public BoxCollider _boxCollider;

    [HideInInspector]
    public CollidersInTrigger _collidersInTrigger;

    [HideInInspector]
    public CanBeSelected _canBeSelected;

    public virtual void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _collidersInTrigger = GetComponent<CollidersInTrigger>();
        _canBeSelected = GetComponent<CanBeSelected>();
    }

    public void Sell()
    {
        FindObjectOfType<MoneyManager>().money += sellPrice;
        Destroy(gameObject);
    }

    public void SetVisuals(bool value)
    {
        visuals.gameObject.SetActive(value);
    }

    public virtual void Activate(bool value)
    {
        GetComponent<IInteractable>().isActivated = value;
    }

    public virtual bool CanBePlaced() { return false; }
}

