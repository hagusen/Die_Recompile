using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerFloor : MonoBehaviour
{
    public WorldGeneration gen;

    

    [Header("Groups")]
    [Tooltip("Chance to spawn a group with an enemy")]
    [Range(0f, 1f)]
    public float chanceForGroup;
    public int minGroupSize;
    public int maxGroupSize;

    [Header("Enemies")]
    public float enemyCount;
    public List<GameObject> enemies;
    public List<float> percent;
    public int floor;

    public List<GameObject> enemyInstances;
    public Bounds bounds;
    private Vector3 min;
    private Vector3 max;

    // Start is called before the first frame update
    void Start()
    {
        //DoStart();
    }

    public void DoStart(bool safeForEditor)
    {
        min.x = gen.tileSize * (gen.tilesPerRow);
        min.z = gen.tileSize * (gen.tilesPerColumn);
        min.y = 0;

        max.x = min.x + (gen.tileSize * (gen.tilesPerRow) * gen.rowsOfRooms);
        max.z = min.z + (gen.tileSize * (gen.tilesPerColumn) * gen.columnsOfRooms);
        max.y = 2;

        enemyInstances = new List<GameObject>();

        bounds.SetMinMax(min, max);

        bounds.center = new Vector3(min.x + ((max.x - min.x)/2) + 300 * floor, 0, min.z + ((max.z - min.z) / 2));
    }

    public void BakeFloor()
    {
        Manager.Instance.navMeshes[floor].Bake();
    }

    void CreateGroup(GameObject inObject)
    {
        for (int i = 0; i < (int)Random.Range(minGroupSize, maxGroupSize); i++)
        {
            GameObject tmp = Instantiate(enemies[0]);
            tmp.transform.position = inObject.transform.position + (Random.insideUnitSphere * 5);

            enemyInstances.Add(tmp);
        }
    }

    void newRandomEnemy()
    {
        float tmp = Random.Range(0, GetTotalPercentage());
        GameObject spawnedUnit;

        float step = 0;

        int i = 0;

        while(true)
        {
            step += percent[i];

            if(tmp <= step)
            {
                spawnedUnit = Instantiate(enemies[i]);
                spawnedUnit.transform.position = GetRandomPos();
                enemyInstances.Add(spawnedUnit);
                break;
            }

            i++;
        }

        if (Random.value < chanceForGroup)
        {
            CreateGroup(spawnedUnit);
        }
    }

    public void SpawnEnemies()
    {
        int i = 0;

        while (i < enemyCount)
        {
            newRandomEnemy();

            i++;
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector3 tmp = new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        0,
        Random.Range(bounds.min.z, bounds.max.z)
        );

        return tmp;
    }

    public void RemoveEnemies()
    {
        foreach (GameObject i in enemyInstances)
        {
            Destroy(i);
        }
        enemyInstances.Clear();
    }

    private float GetTotalPercentage()
    {
        float temp = 0;

        foreach(float i in percent)
        {
            temp += i;
        }

        return temp;
    }

    public void IdleEnemies()
    {
        foreach (GameObject i in enemyInstances)
        {
            if (i) {
                Destroy(i.GetComponent<Enemy>());
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.red;
        Gizmos.DrawCube(bounds.center, new Vector3(bounds.size.x, bounds.size.y, bounds.size.z));
    }
}
