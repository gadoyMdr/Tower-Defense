using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public void Resume()
    {
        PauseManager.isPaused = false;
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void SaveAndQuit()
    {
        Time.timeScale = 1f;
        GameSaveManager.Instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}
