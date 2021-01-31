using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemySpawnerFloor> floors;

    public void SpawnEnemies(int inData)
    {
       // floors[inData].SpawnEnemies();
    }

    public void BakeFloor(int inData)
    {
        floors[inData].BakeFloor();
    }

    public void RemoveEnemies(int inData)
    {
        floors[inData].RemoveEnemies();
    }

    public void DoStart(bool safeForEditor)
    {
        foreach(EnemySpawnerFloor i in floors)
        {
            i.DoStart(safeForEditor);
        }
    }
}
