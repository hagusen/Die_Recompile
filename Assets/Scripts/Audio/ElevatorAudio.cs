using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ElevatorAudio : MonoBehaviour
{
    [EventRef]
    public string elevatorOpenEvent;
    EventInstance elevatorOpen;

    /*

    // spelas upp när hiss-dörren öppnas
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            elevatorOpen = RuntimeManager.CreateInstance(elevatorOpenEvent);
            elevatorOpen.start();
        }
        //RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    
    }
    */


    public void PlayOnElevatorOpen()
    {
        elevatorOpen = RuntimeManager.CreateInstance(elevatorOpenEvent);
        elevatorOpen.start();

    }

}
