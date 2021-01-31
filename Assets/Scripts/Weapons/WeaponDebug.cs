using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDebug : MonoBehaviour {

    public GameObject[] weapons;

    public bool cheatsEnabled;


    private bool cheats = true;
    private bool cheats2 = false;
    private bool cheats3 = false;




    // Start is called before the first frame updatSe
    void Start() {

    }

    // Update is called once per frame
    void Update() {




        if (cheats) {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.N)) {
                cheats2 = true;
            }
        }

        if (cheats2) {
            if (Input.GetKey(KeyCode.Tab)) {
                cheats3 = true;

            }
        }



        if (cheats3 || cheatsEnabled) {
            if (Input.GetKey(KeyCode.Y) || cheatsEnabled) {
                for (int i = 0; i < weapons.Length; i++) {
                    if (Input.GetKeyDown(KeyCode.Alpha0 + i)) {

                        Instantiate(weapons[i], transform.position, Quaternion.identity);
                    }
                }

                if (Input.GetKey(KeyCode.P)) {
                    GetComponent<PlayerStats>().maxHp = 1337420;
                    GetComponent<PlayerStats>().stats.health = 1337420;
                    GetComponent<PlayerStats>().uihealth.Ping(1337420);
                }

            }



        }







    }
}
