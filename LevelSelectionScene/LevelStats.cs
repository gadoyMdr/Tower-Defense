using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapStats")]
public class LevelStats : ScriptableObject
{
    public GameObject map;
    public new string name;
    public int tries = 0;
    public bool done = false;
}
