using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BigBrainAudio : MonoBehaviour
{
    /*
    [EventRef]
    public string enemyAgroEvent;
    EventInstance enemyAgro;

    [EventRef]
    public string enemyDeathEvent;
    EventInstance enemyDeath;

    [EventRef]
    public string enemyHurtEvent;
    EventInstance enemyHurt;
    */
    public enum BrainStepsType {

        GalaxyBrain,
        BigBrain,
        SmallBrain

    }

    public BrainStepsType bigbrainType;


    public void PlayBigbrainStep(string path)
    {
        switch (bigbrainType) {
            case BrainStepsType.GalaxyBrain:

                RuntimeManager.PlayOneShot("event:/SFX/Enemies/Big Brain/BigBrain_Movement", GetComponent<Transform>().position);
                break;
            case BrainStepsType.BigBrain:

                RuntimeManager.PlayOneShot("event:/SFX/Enemies/Big Brain/BigBrain_Movement", GetComponent<Transform>().position);
                break;
            case BrainStepsType.SmallBrain:

                RuntimeManager.PlayOneShot("event:/SFX/Enemies/Big Brain/BigBrain_Movement 2", GetComponent<Transform>().position);
                break;

            default:
                break;
        }
    }

    public void PlayBigBrainAttack(string path)
    {
        RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    /*
    public void PlayBigbrainAgro()
    {
        enemyAgro = RuntimeManager.CreateInstance(enemyAgroEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyAgro, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyAgro.start();
    }

    public void PlayBigbrainHurt()
    {
        enemyHurt = RuntimeManager.CreateInstance(enemyHurtEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyHurt, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyHurt.start();
    }
    
    public void PlayBigbrainDeath()
    {
        enemyHurt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        enemyAgro.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        enemyDeath = RuntimeManager.CreateInstance(enemyDeathEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyDeath, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyDeath.start();

    }
    */
}
