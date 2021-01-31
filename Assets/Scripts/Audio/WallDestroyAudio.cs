using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class WallDestroyAudio : MonoBehaviour
{
    [EventRef]
    public string wallDestroyEvent;
    EventInstance wallDestroy;

    // ska spelas när väggar förstörs
    public void PlayExplosion()
    {
        wallDestroy = RuntimeManager.CreateInstance("event:/SFX/Interacibles/Wall_destruction");
        RuntimeManager.AttachInstanceToGameObject(wallDestroy, GetComponent<Transform>(), GetComponent<Rigidbody>());
        wallDestroy.start();
    }
}
