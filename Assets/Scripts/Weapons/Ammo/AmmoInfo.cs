using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//CREATED BY "John Boman"
//USED FOR "Storing Ammo Info"

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AmmoInfo")]
public class AmmoInfo : ScriptableObject {





    public Ammo[] ammos = new Ammo[4];

    [Header("General")]
    [Range(0,1)]
    public float AmmoFromPickup;
    [Range(0, 1)]
    public float AmmoFromWeapon;

    [Header("Ammo Spawner")]
    [Range(0, 1)]
    public float preferMainWeapon;
    [Range(0, 1)]
    public float ammoDropchance;
    [Range(0, 1)]
    public float healthDropChance;
    [Range(0, 1)]
    public float resourceDropChance;

    [Header("Note: Normal ammo percent + this add percent (+ they stack)")]
    public AmmoPercent[] extraAmmo;
    public float pickupLifetime = 10;


    // 0 - 1 percent of max ammo
    // ammo from new weapon pickup
    // ammo from pickup

    //public float 








    void OnValidate() {


        for (int i = 0; i < ammos.Length; i++) {
            ammos[i].type = (AmmoType)i;
        }

    }

    [Serializable]
    public struct AmmoPercent {
        [Range(0,1)]
        public float under;
        [Range(0, 1)]
        public float add;
        
    }

}




[Serializable]
public struct Ammo {


    public AmmoType type;
    public int maxAmmo;
    public int curAmmo;

}


public enum AmmoType {
    Bullet,
    Shell,
    Explosive,
    Energy,
    Enemy
}
