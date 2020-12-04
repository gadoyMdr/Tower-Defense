using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionSystem : MonoBehaviour
{
    WorldRaycast worldRaycast;
    PlaceObject placeObject;

    private CanBeSelected lastInteractedWith;
    private CanBeSelected lastSelected;

    
    private void Awake()
    {
        worldRaycast = FindObjectOfType<WorldRaycast>();
        placeObject = FindObjectOfType<PlaceObject>();
    }

    private void Update() => CheckToOutline();
    

    void CheckToOutline()
    {
        if (PauseManager.isPaused) return;

        bool isOverMenu = worldRaycast.allUIs.Any(x => x.gameObject.name.Contains("Sub"));

        CanBeSelected current = null;
        if (worldRaycast.gameObjectHit == null? false : worldRaycast.gameObjectHit.TryGetComponent(out current) && !placeObject.isAllowedToPlace)
        {
            

            if(!current.isSelected)
                current.isHovered = true;

            if (Input.GetMouseButtonDown(0) && !isOverMenu)
            {
                if (lastSelected != null)
                    lastSelected.isSelected = false;
                
                current.isSelected = true;

                if(placeObject.objectToPlaceInstance != null)
                    Destroy(placeObject.objectToPlaceInstance.gameObject);

                lastSelected = current;

            }

            lastInteractedWith = current;
        }
        else
        {
            if(lastInteractedWith != null && !isOverMenu)
            {
                if (!lastInteractedWith.isSelected)
                    lastInteractedWith.isHovered = false;
                
                    

                if (Input.GetMouseButtonDown(0))
                    lastSelected.isSelected = false;
            }
        }
    }
}
