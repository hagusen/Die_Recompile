using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerAudio : MonoBehaviour
{
   

    [EventRef]
    public string playerHurtEvent;
    EventInstance playerHurt;

    [EventRef]
    public string playerDeathEvent;
    EventInstance playerDeath;

    [EventRef]
    public string playerDashEvent;
    EventInstance playerDash;
    public float dashDuration = 0.1f;
    public float dashIncrement = 0.01f;

    [EventRef]
    public string playerSpawnEvent;
    EventInstance playerSpawn;


    [EventRef]
    public string switchWeaponEvent;
    EventInstance switchWeapon;

    [EventRef]
    public string grabWeaponEvent;
    EventInstance grabWeapon;

    [EventRef]
    public string healthPickupEvent;
    EventInstance healthPickup;

    [EventRef]
    public string noAmmoEvent;
    EventInstance noAmmo;

    [EventRef]
    public string lowAmmoEvent;
    EventInstance lowAmmo;
    public float lowAmmoTimer = 1.0f;
    float timer = 200;
    // spelas upp när spelaren går

    public enum PlayerStepsType
    {

        Player,
        EvilPlayer,


    }

    public PlayerStepsType playerType;


    public void PlayOnPlayerMove(string path) 
    {
        switch (playerType)
        {
            case PlayerStepsType.Player:
                RuntimeManager.PlayOneShot("event:/SFX/Player/Movement/Player_Footsteps", GetComponent<Transform>().position);
                break;
            case PlayerStepsType.EvilPlayer:
                RuntimeManager.PlayOneShot("event:/SFX/Enemies/Zombie_Player/Zombie_Player_Movement", GetComponent<Transform>().position);
                break;

            default:
                break;
        }
        
        //playerFootstep = RuntimeManager.CreateInstance(playerFootstepEvent);
        //playerFootstep.start();

        DustWalk();
    }

    void DustWalk() {
        var temp = EffectPool.ins.Get((int)EffectType.Dust_walk);
        temp.SetValues(transform.position);
        temp.gameObject.SetActive(true);
    }

    IEnumerator Dash(float dur, float increment) {
        float timer = 0;

        while (dur > timer) {
            DustWalk();
            timer += increment;
            yield return new WaitForSeconds(increment);
        }
        yield return null;
    }

    // spelas upp när spelaren tar skada
    public void PlayOnPlayerHurt()
    {
        playerHurt = RuntimeManager.CreateInstance(playerHurtEvent);
        RuntimeManager.AttachInstanceToGameObject(playerHurt, GetComponent<Transform>(), GetComponent<Rigidbody>());
        playerHurt.start();
    }

    // spelas upp när spelaren dör
    public void PlayOnPlayerDeath()
    {
        playerDeath = RuntimeManager.CreateInstance(playerDeathEvent);
        playerHurt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        
        RuntimeManager.AttachInstanceToGameObject(playerDeath, GetComponent<Transform>(), GetComponent<Rigidbody>());
        playerDeath.start();
    }

    // spelas upp när spelaren använder dash
    public void PlayOnPlayerDash()
    {
        playerDash = RuntimeManager.CreateInstance(playerDashEvent);
        playerDash.start();
        /*
        var temp = EffectPool.ins.Get((int)EffectType.Dust_Dash);
        temp.SetValues(transform.position);
        temp.gameObject.SetActive(true);
        */
        StartCoroutine(Dash(dashDuration, dashIncrement));
        
    }

    //spelas upp när spelaren spawnar
    public void PlayOnPlayerSpawn()
    {
        playerSpawn = RuntimeManager.CreateInstance(playerSpawnEvent);
        playerSpawn.start();
    }

    //spelas upp när spelaren byter vapen
    public void PlaySwitchWeapon() 
    {
        switchWeapon = RuntimeManager.CreateInstance(switchWeaponEvent);
        switchWeapon.start();
    }

    // spelas upp när spelaren tar upp ett vapen
    public void PlayGrabWeapon() 
    {
        grabWeapon = RuntimeManager.CreateInstance(grabWeaponEvent);
        grabWeapon.start();
    }

    public void PlayHealthPickup() 
    {
        healthPickup = RuntimeManager.CreateInstance(healthPickupEvent);
        healthPickup.start();
    }

    public void PlayNoAmmoShot() {
        noAmmo = RuntimeManager.CreateInstance(noAmmoEvent);
        noAmmo.start();
    }
    
    public void PlayLowAmmo() // Calls every frame when low ammo
    {

        timer += Time.deltaTime;

        if (lowAmmoTimer < timer) {
            lowAmmo = RuntimeManager.CreateInstance(lowAmmoEvent);
            lowAmmo.start();
            timer = 0;
        }
    }


}
