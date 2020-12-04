using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private float degreeSteps = 5;

    private MyObjectToPlace ObjectToPlacePrefab;
    
    public MyObjectToPlace objectToPlacePrefab
    {
        get => ObjectToPlacePrefab;
        set
        {
            ObjectToPlacePrefab = value;
            if (objectToPlaceInstance != null) Destroy(objectToPlaceInstance.gameObject);
        }
    }

    private bool IsAllowedToPlace;
    public bool isAllowedToPlace
    {
        get => IsAllowedToPlace;
        set
        {
            IsAllowedToPlace = value;
            if (!value)
            {
                if (objectToPlaceInstance != null) objectToPlaceInstance.SetVisuals(false);
            }
        }
    }

    [HideInInspector]
    public MyObjectToPlace objectToPlaceInstance;

    SlotSelection slotSelection;
    WorldRaycast worldRaycast;
    MoneyManager moneyManager;

    bool canPlace = true;
    

    private void Awake()
    {
        
        slotSelection = FindObjectOfType<SlotSelection>();
        worldRaycast = FindObjectOfType<WorldRaycast>();
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    private void OnEnable() => slotSelection.HotBarSelection += _ => { isAllowedToPlace = true; objectToPlaceInstance = Instantiate(objectToPlacePrefab, worldRaycast.point, Quaternion.identity); } ;

    void Update()
    {
        PlaceLogic();
    }

    void PlaceLogic()
    {
        if (PauseManager.isPaused) return;

        if (worldRaycast.gameObjectHit == null ? false : worldRaycast.gameObjectHit.CompareTag("Ally") || worldRaycast.isMouseOverUI) isAllowedToPlace = false;
        else isAllowedToPlace = true;

        CheckIfSpawningNewObject();

        if (objectToPlaceInstance != null)
        {
            if (!isAllowedToPlace) objectToPlaceInstance.SetVisuals(false);
            else objectToPlaceInstance.SetVisuals(true);

            Preview();

            if (Input.GetKey(KeyCode.LeftControl))
                Rotate();
            else if (Input.GetMouseButtonDown(0) && canPlace && moneyManager.money - objectToPlaceInstance.price >= 0)
                PlaceObjectMethod();

        }

    }

    void CheckIfSpawningNewObject()
    {
        if(!worldRaycast.isMouseOverUI && worldRaycast.point != Vector3.zero)
        {
            if (objectToPlaceInstance != null)
                objectToPlaceInstance.SetVisuals(true);
        }
        else
        {
            if (objectToPlaceInstance != null) objectToPlaceInstance.SetVisuals(false);
        }
    }

    void Rotate()
    {
        if(Input.mouseScrollDelta.y > 0)
            objectToPlaceInstance.transform.eulerAngles = new Vector3(0, objectToPlaceInstance.transform.eulerAngles.y + degreeSteps , 0);
        else if (Input.mouseScrollDelta.y < 0)
            objectToPlaceInstance.transform.eulerAngles = new Vector3(0, objectToPlaceInstance.transform.eulerAngles.y - degreeSteps, 0);
    }

    void Preview()
    {
        CheckIfCollide(out canPlace);
        objectToPlaceInstance.transform.position = worldRaycast.point;
        objectToPlaceInstance.Activate(false);
    }

    void PlaceObjectMethod()
    {
        objectToPlaceInstance.gameObject.layer = LayerMask.NameToLayer("Tower");
        moneyManager.money -= objectToPlaceInstance.price;
        objectToPlaceInstance.Activate(true);

        objectToPlaceInstance = null;
        IsAllowedToPlace = false;
    }

    void CheckIfCollide(out bool canBePlaced)
    {
        if(objectToPlaceInstance.CanBePlaced())
        {
            canBePlaced = true;
            objectToPlaceInstance._canBeSelected.outlineSpecs = OutlineSpecs.GreenThick;
        }
        else
        {
            canBePlaced = false;
            objectToPlaceInstance._canBeSelected.outlineSpecs = OutlineSpecs.RedThick;
        }
    }
}
