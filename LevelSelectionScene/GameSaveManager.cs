using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager
{
    

    public LevelStats levelStats;

    private const string gameDir = "/GameSave";


    private static GameSaveManager instance = null;
    public static GameSaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameSaveManager();
            }
            return instance;
        }
    }

    public bool IsSavedFile()
    {
        return Directory.Exists(Application.persistentDataPath + gameDir);
    }

    //I'm about to die of fatigue
    void CreateDirectories()
    {
        Directory.CreateDirectory($"{Application.persistentDataPath}/GameSave/The Following");
        Directory.CreateDirectory($"{Application.persistentDataPath}/GameSave/The Beginning");
    }

    public void SaveGame()
    {
        
        if (!IsSavedFile())
            Directory.CreateDirectory(Application.persistentDataPath + gameDir);
        if (!Directory.Exists($"{Application.persistentDataPath}{gameDir}/{levelStats.name}"))
            Directory.CreateDirectory($"{Application.persistentDataPath}{gameDir}/{levelStats.name}");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create($"{Application.persistentDataPath}{gameDir}/{levelStats.name}/save.txt");
        string json = JsonUtility.ToJson(levelStats);
        binaryFormatter.Serialize(file, json);
        file.Close();
    }

    public LevelStats LoadGame(string mapName, out LevelStats stats)
    {
        CreateDirectories();

        LevelStats temp = new LevelStats();
        stats = null;
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (!Directory.Exists($"{Application.persistentDataPath}{gameDir}/{mapName}"))
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}{gameDir}/{mapName}");
            return null;
        }
            

        if (File.Exists($"{Application.persistentDataPath}{gameDir}/{mapName}/save.txt"))
        {
            FileStream file = File.Open($"{Application.persistentDataPath}{gameDir}/{mapName}/save.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), temp);
            file.Close();
        }
        stats = temp;
        return temp;
    }
}
