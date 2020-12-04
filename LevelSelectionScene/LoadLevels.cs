using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevels : MonoBehaviour
{
    public List<LevelStats> allLevels = new List<LevelStats>();

    [SerializeField]
    private LevelUIPrefab levelUIPrefab;

    [SerializeField]
    private Transform content;

    void Start()
    {
        InstantiateLevelPrefabs();
    }

    void InstantiateLevelPrefabs()
    {
        GameSaveManager.Instance.levelStats = allLevels[0];
        int counter = 0;
        foreach(LevelStats l in allLevels)
        {
            Debug.Log(allLevels[counter].name);
            Instantiate(levelUIPrefab, content)
            .SetParameters(GameSaveManager.Instance.LoadGame(l.name, out LevelStats ll).map == null ? allLevels[counter] : ll);
            counter++;
        }
    }
}
