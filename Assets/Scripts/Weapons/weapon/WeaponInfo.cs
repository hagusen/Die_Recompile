using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREATED BY "John Boman"
//USED FOR "Storing different weapons info"


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponInfo")]
public class WeaponInfo : ScriptableObject {

    public string weaponName;


    public WeaponType weaponType;
    
    [Header("Basics")]
    public BulletInfo bulletInfo;
    [Min(0)]
    public float reloadTime;
    [Min(0)]
    public float knockbackForce;
    [Min(0)]
    public float CameraRecoilForce = 3;
    [Min(0)]
    public float CameraRecoilDuration = 0.1f;
    [Range(0, 360)]
    public float bulletSpread;
    //[Min(0)]
    //public float bulletSpreadOverTime = 1; //animation curve on request //ex 1.2 multiply spread per sec?
    [Min(0)]
    public int ammoLostPerShot = 1;


    [Header("wallcheck")]
    [Min(0)]
    public float wallcheckDistance = .5f;

    //burst
    [Header("Burst Fire")]
    [Min(1)]
    public int fireTimes = 1;
    [Min(0)]
    public float timeBetweenFires = 0.5f;

    //
    [Header("Multishot")]
    [Range(0,360)]
    public float firingAngle;
    [Min(1)]
    public int firingAmount;

    [Header("Arc")]
    [Min(0)]
    public float arcTime = 1; //constant
    public float arcHeight = 10;
    //Max range?


#if UNITY_EDITOR
    [Space(40, order = 0)]
    [Header("EDITOR ONLY", order = 1)]
    [Tooltip("Write Any notes here: (example: this is the bullet for the shotgun etc.)")]
    [TextArea(5, 40)]
    public string notesForDesigners;

#endif



    // calc knockback [knockback * firing speed]
    // per sec
    // per shot

    // firing angle (0-45)
    // firing amount
    // angle per shot?

    // max ammo x

    void OnValidate() {
        if (!bulletInfo) { // not working...
            Debug.LogWarning("No bullet info chosen " + Faces.GetFace(faceType.Weird, 2));
        }

    }

}

public enum WeaponType {
    Standard,
    Arc,
    Hitscan,
    Melee
}

