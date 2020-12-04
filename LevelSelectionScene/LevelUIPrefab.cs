using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIPrefab : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI tries;

    [SerializeField]
    private TextMeshProUGUI isDone;

    private GameObject map;

    private LevelStats levelStats;
    public void SetParameters(LevelStats levelStats)
    {
        this.title.text = $"Map : {levelStats.name}";
        this.tries.text = $"Tries : {levelStats.tries}";
        this.isDone.text = $"Done : {levelStats.done}";
        this.map = levelStats.map;

        
        this.levelStats = levelStats;
    }

    public void LoadLevel()
    {
        
        
        LoadMap.map = map;

        
        GameSaveManager.Instance.levelStats = levelStats;
        GameSaveManager.Instance.levelStats.tries++;
        GameSaveManager.Instance.SaveGame();

        SceneManager.LoadScene("PlayScene");
        
        SceneManager.LoadScene(levelStats.name, LoadSceneMode.Additive);
        
    }
}
