using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshBake : MonoBehaviour
{

    [SerializeField]
    NavMeshSurface surface;
    public EnemySpawnerFloor spawner;

    public NavMeshData tmp;

    public void Bake()
    {
        Debug.Log("Baking");

        surface.BuildNavMesh();
        tmp = surface.navMeshData;
        spawner.SpawnEnemies();
    }

    public void UpdateMesh()
    {
        tmp = surface.navMeshData;
        surface.UpdateNavMesh(tmp);
    }
}