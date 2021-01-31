using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ImpactAudio : MonoBehaviour
{

    [EventRef]
    public string impactAudioEvent;
    EventInstance impactAudio;

    public void PlayImpact() 
    {
        impactAudio = RuntimeManager.CreateInstance("event:/SFX/Interacibles/Impacts/Wall Impact");
        RuntimeManager.AttachInstanceToGameObject(impactAudio, GetComponent<Transform>(), GetComponent<Rigidbody>());
        impactAudio.start();
    }

}
