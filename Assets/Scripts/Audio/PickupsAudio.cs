using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PickupsAudio : MonoBehaviour
{
    [EventRef]
    public string pickupsSFXEvents;
    EventInstance pickupsSFX;

    //spelas upp när spelaren plockar upp en pickup
    public void PlayOnPickup()
    {
        pickupsSFX = RuntimeManager.CreateInstance(pickupsSFXEvents);
        pickupsSFX.start();
    }


}
