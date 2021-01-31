using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class WeaponAudio : MonoBehaviour
{
    [EventRef]
    public string weaponFireEvent;
    EventInstance WeaponFire;

    [EventRef]
    public string weaponStopEvent;
    EventInstance WeaponStop;

    public bool OnStopFire = false;

    //ska ligga på varje vapen prefab 
    public void PlayAudio() 
    {
        WeaponFire = RuntimeManager.CreateInstance(weaponFireEvent);
        RuntimeManager.AttachInstanceToGameObject(WeaponFire, GetComponent<Transform>(), GetComponent<Rigidbody>());
        WeaponFire.start();
        //print(Faces.GetFace(faceType.Party, 2));
    }

    public void PlayOnStopFire() {
        if (OnStopFire)
        {

            WeaponStop = RuntimeManager.CreateInstance(weaponStopEvent);
            RuntimeManager.AttachInstanceToGameObject(WeaponStop, GetComponent<Transform>(), GetComponent<Rigidbody>());
            WeaponStop.start();
        }


    }


}
