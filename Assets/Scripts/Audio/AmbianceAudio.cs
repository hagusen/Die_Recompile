using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AmbianceAudio : MonoBehaviour
{

    [EventRef]
    public string ambLabEvent;
    EventInstance ambLab;

    [EventRef]
    public string ambLevelOneEvent;
    EventInstance ambLevelOne;

    [EventRef]
    public string ambLevelTwoEvent;
    EventInstance ambLevelTwo;

    [EventRef]
    public string ambLevelThreeEvent;
    EventInstance ambLevelThree;

    //ska spelas upp i labb-zonen och stängas av när spelaren lämnar
    public void PlayAmbLab ()
    {    
            ambLevelOne.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            ambLab = RuntimeManager.CreateInstance(ambLabEvent);
            ambLab.start();    
    }

    // ska spelas upp när spelaren är i första zonen
    public void PlayAmbOne ()
    {
            ambLab.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            ambLevelOne = RuntimeManager.CreateInstance(ambLevelOneEvent);
            ambLevelOne.start();
    }

    // ska spelas upp när spelaren är i andra zonen
    public void PlayAmbTwo ()
    {
        ambLevelOne.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambLevelTwo = RuntimeManager.CreateInstance(ambLevelTwoEvent);
        ambLevelTwo.start();
    }

    // ska spelas upp när spelaren är i tredje zonen
    public void PlayAmbThree ()
    {
        ambLevelTwo.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambLevelThree = RuntimeManager.CreateInstance(ambLevelThreeEvent);
        ambLevelThree.start();
    }


}
