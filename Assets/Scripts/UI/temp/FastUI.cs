using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastUI : MonoBehaviour {



    
    public AmmoInfo info;

    public Text curAmmo;
    public Text maxAmmo;




    void Start() {

        AmmoManager.OnCurrentAmmoChanged += ChangeCurrentAmmo;
        WeaponManager.OnWeaponTypeChanged += ChangeMaxAmmo;

    }


    void ChangeMaxAmmo(AmmoType type) {
        maxAmmo.text = info.ammos[(int)type].maxAmmo.ToString();
    }

    void ChangeCurrentAmmo(int x) {
        curAmmo.text = x.ToString();

    }

}


