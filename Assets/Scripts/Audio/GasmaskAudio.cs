using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class GasmaskAudio : MonoBehaviour
{

    [EventRef]
    public string enemyAgroEvent;
    EventInstance enemyAgro;

    [EventRef]
    public string enemyDeathEvent;
    EventInstance enemyDeath;

    [EventRef]
    public string enemyHurtEvent;
    EventInstance enemyHurt;

    

    void PlayGasmaskStep (string path)
    {
        RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    public void PlayGasmaskAttack (string path)
    {
        RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    public void PlayGasmaskAgro()
    {
        enemyAgro = RuntimeManager.CreateInstance(enemyAgroEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyAgro, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyAgro.start();
    }

    public void PlayGasmaskHurt()
    {
        enemyHurt = RuntimeManager.CreateInstance(enemyHurtEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyHurt, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyHurt.start();
    }
    
    public void PlayGasmaskDeath()
    {
        enemyHurt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        enemyAgro.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        enemyDeath = RuntimeManager.CreateInstance(enemyDeathEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyDeath, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyDeath.start();

    }
}
