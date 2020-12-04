using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainBar : MonoBehaviour
{
    [SerializeField]
    private MainSlot mainSlotPrefab;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private Image selectedSlotPrefab;

    public List<MyObjectToPlace> myObjects = new List<MyObjectToPlace>();

    private List<AllyType> allyTypes = new List<AllyType>();

    private SlotSelection slotSelection;
    private PlaceObject placeObject;
    private Image selectedSlotInstance;

    private void Awake()
    {
        slotSelection = FindObjectOfType<SlotSelection>();
        placeObject = FindObjectOfType<PlaceObject>();

        selectedSlotInstance = Instantiate(selectedSlotPrefab);
        Initiate();
    }

    private void OnEnable() => slotSelection.HotBarSelection += ChangeSlot;

    private void OnDisable() => slotSelection.HotBarSelection -= ChangeSlot;

    

    void Initiate()
    {
        int counter = 0;
        foreach(MyObjectToPlace obj in myObjects)
        {
            if (!allyTypes.Contains(obj.allyType))
                allyTypes.Add(obj.allyType);
        }

        foreach(AllyType allyType in allyTypes)
        {
            MainSlot instance = Instantiate(mainSlotPrefab, content);
            instance.SetObjects(myObjects.Where(x => x.allyType.Equals(allyType)).ToList());
            instance.SetParameters(counter);

            counter++;
        }
    }

    void ChangeSlot(int id)
    {
        MainSlot selectedSlot = FindObjectsOfType<MainSlot>().Where(x => x.ID == id).FirstOrDefault();

        if(selectedSlot != null)
        {
            selectedSlotInstance.gameObject.transform.SetParent(selectedSlot.transform);
            selectedSlotInstance.gameObject.transform.SetAsFirstSibling();
            selectedSlotInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            placeObject.objectToPlacePrefab = selectedSlot.currentObject;

            Utils.DestroySubSlots();
        }
            
    }
}
