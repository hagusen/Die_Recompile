using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemyAudio : MonoBehaviour
{

    [EventRef]
    public string enemyAgroEvent;
    EventInstance enemyAgro;

    [EventRef]
    public string enemyAttackEvent;
    EventInstance enemyAttack;

    [EventRef]
    public string enemyDeathEvent;
    EventInstance enemyDeath;

    [EventRef]
    public string enemyHurtEvent;
    EventInstance enemyHurt;


    // spelas upp när fienden är redo för att attackera
    public void PlayOnEnemyAttack ()
    {
        enemyAgro.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        enemyAttack = RuntimeManager.CreateInstance(enemyAttackEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyAttack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyAttack.start();
    }

    // spelas upp när fienden märker av spelaren och börjar röra på sig
    public void PlayOnEnemyAgro ()
    {
        enemyAgro = RuntimeManager.CreateInstance(enemyAgroEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyAgro, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyAgro.start();
    }

    public void PlayOnEnemyHurt()
    {
        enemyHurt = RuntimeManager.CreateInstance(enemyHurtEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyHurt, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyHurt.start();
    }

    // spelas upp när fienden dör
    public void PlayOnEnemyDeath ()
    {
        enemyHurt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        enemyAgro.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        enemyAttack.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        enemyDeath = RuntimeManager.CreateInstance(enemyDeathEvent);
        RuntimeManager.AttachInstanceToGameObject(enemyDeath, GetComponent<Transform>(), GetComponent<Rigidbody>());
        enemyDeath.start();

        enemyAgro.triggerCue();
    }


}
