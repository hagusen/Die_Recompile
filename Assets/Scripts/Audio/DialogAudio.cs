using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DialogAudio : MonoBehaviour
{
    [EventRef]
    public string thomasDialogEvent;
    EventInstance thomasDialog;

    [EventRef]
    public string corneliaDialogEvent;
    EventInstance corneliaDialog;

    [EventRef]
    public string ritaDialogEvent;
    EventInstance ritaDialog;

    [EventRef]
    public string playerDialogEvent;
    EventInstance playerDialog;

    // spelas upp tillsammans med Thomas dialog
    public void PlayOnThomasDialog ()
    {
        thomasDialog = RuntimeManager.CreateInstance(thomasDialogEvent);
        thomasDialog.start();
    }

    // spelas upp tillsammans med Cornelias dialog
    public void PlayOnCorneliaDialog ()
    {
        corneliaDialog = RuntimeManager.CreateInstance(corneliaDialogEvent);
        corneliaDialog.start();
    }
    
    // spelas upp tillsammans med Ritas dialog
    public void PlayOnRitaDialog ()
    {
        ritaDialog = RuntimeManager.CreateInstance(ritaDialogEvent);
        ritaDialog.start();
    }

    // spelas upp samtidigt med spelarens dialgo
    public void PlayOnPlayerDialog ()
    {
        playerDialog = RuntimeManager.CreateInstance(playerDialogEvent);
        playerDialog.start();
    }


}
