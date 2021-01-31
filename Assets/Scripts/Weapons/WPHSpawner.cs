using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPHSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct WeaponTier {
        public GameObject[] prefabs;
    }

    public WeaponTier[] Floors;

    List<Vector3>[] positions;

    List<GameObject> curObjs = new List<GameObject>();

    public static WPHSpawner ins;


    public void Begin(List<Vector3>[] poses) {
        positions = poses;

        SpawnWeapons();
    }


    public void ResetWeapons() {
        DestroyWPHs();
        SpawnWeapons();
    }

    void SpawnWeapons() {

        print("TWO TIIMES0");
        if (positions != null) {
            for (int i = 0; i < positions.Length; i++) {

            for (int j = 0; j < positions[i].Count; j++) {
                if (Random.Range(0f,1f) >= 0.985f) {
                        GameObject s = Floors[i].prefabs[Random.Range(0, Floors[i].prefabs.Length)];
                    var x = Instantiate(s, positions[i][j] + new Vector3(0,1.2f,0), Quaternion.identity);
                   curObjs.Add(x);

                }
            }
            }
        }

    }
    void DestroyWPHs() {

        foreach (var item in curObjs) {
            Destroy(item.gameObject);
        }
        curObjs = new List<GameObject>();
    }





    // Start is called before the first frame update
    void Awake()
    {
        if (!ins) {
            ins = this;
        }
    }

}
