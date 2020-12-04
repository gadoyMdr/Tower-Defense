using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EscapeKeyManager : MonoBehaviour
{

    PlaceObject placeObject;
    PauseManager pauseManager;

    private void Awake()
    {
        placeObject = FindObjectOfType<PlaceObject>();
        pauseManager = FindObjectOfType<PauseManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            DoWhatever();
    }

    void DoWhatever()
    {
        List<CanBeSelected> canBeSelected = new List<CanBeSelected>(FindObjectsOfType<CanBeSelected>().Where(x => x.isSelected).ToList());
        List<ControlTower> controlledTower = new List<ControlTower>(FindObjectsOfType<ControlTower>().Where(x => x.isControlled).ToList());

        bool canPause = true;

        if (controlledTower.Count > 0)
        {
            controlledTower.ForEach(x => x.isControlled = false);
            canPause = false;
        }

        if (placeObject.objectToPlaceInstance != null)
        {
            Destroy(placeObject.objectToPlaceInstance.gameObject);
            canPause = false;
        }
            
        if (canBeSelected.Count > 0)
        {
            canBeSelected.ForEach(x => x.isSelected = false);
            canPause = false;
        }

        if (canPause)
            pauseManager.Pause();


    }
}
