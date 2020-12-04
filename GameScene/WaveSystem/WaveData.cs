using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Wave")]
public class WaveData : ScriptableObject
{
    public RandomGOInSet randomEnemies;

    [Range(0.1f, 5)]
    public float spawnTime = 3;

    [Range(1, 80)]
    public int enemyCount;
}
