using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class VerticalSubMenu : MonoBehaviour, IPointerExitHandler
{
    [SerializeField]
    private SubSlot subSlotPrefab;

    public List<MyObjectToPlace> objectList = new List<MyObjectToPlace>();

    [HideInInspector]
    public MainSlot parentMainSlot;

    [HideInInspector]
    public bool isUnFold;

    public List<SubSlot> subSlots = new List<SubSlot>();
    public void Unfold(MyObjectToPlace current)
    {
        parentMainSlot.HideVisuals(false);
        isUnFold = true;

        foreach (MyObjectToPlace o in objectList)
        {
            SubSlot newInstance = Instantiate(subSlotPrefab, this.transform);
            newInstance.SetObjectAndParent(o, this);

            subSlots.Add(newInstance);   
        }

        subSlots.Where(x => x.currentObject.Equals(current)).FirstOrDefault().transform.SetAsLastSibling();
    }

    public void Select(MyObjectToPlace objectTo)
    {
        parentMainSlot.SetObject(objectTo);
        Fold();
    }

    public void Fold()
    {
        parentMainSlot.HideVisuals(true);
        isUnFold = false;

        Utils.DestroySubSlots();

        subSlots.Clear();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Fold();
    }
}
