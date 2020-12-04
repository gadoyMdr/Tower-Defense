using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionUIManager : MonoBehaviour
{
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
