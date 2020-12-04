using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;

    [SerializeField]
    private InGameMenuManager inGameMenuManagerPrefab;

    private InGameMenuManager inGameMenuManagerInstance;

    public void Pause()
    {
        if (!isPaused)
            ActualPause();
        else
            inGameMenuManagerInstance.Resume();
    }

    void ActualPause()
    {
        isPaused = true;
        Time.timeScale = 0;
        inGameMenuManagerInstance = Instantiate(inGameMenuManagerPrefab);
    }
}
