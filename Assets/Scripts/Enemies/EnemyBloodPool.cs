using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBloodPool : MonoBehaviour
{
    public GameObject basePool;

    public List<Material> materials;
    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }

    public GameObject CreatePool()
    {
        GameObject pool = Instantiate(basePool);
        pool.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Count)];
        float tmp = Random.rotation.eulerAngles.y;
        pool.transform.rotation = Quaternion.Euler(90,tmp,0);

        return pool;
    }
}
