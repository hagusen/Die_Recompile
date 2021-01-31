using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class ExplosionAudio : MonoBehaviour
{
    [EventRef]
    public string explosionEvent;
    EventInstance explosionSfx;

    //ska spelas tillsammans med explosions prefab
    public void PlayExplosion()
    {
        explosionSfx = RuntimeManager.CreateInstance(explosionEvent);
        RuntimeManager.AttachInstanceToGameObject(explosionSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
        explosionSfx.start();
    }

}
