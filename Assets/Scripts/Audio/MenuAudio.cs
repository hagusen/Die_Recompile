using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MenuAudio : MonoBehaviour
{
    
     EventInstance menuSelect;

     
     EventInstance menuPressStart;

     
     EventInstance menuPressOptions;

    // spelas upp när man är över en menu knapp
    public void PlayOnMenuSelect()
    {
        menuSelect = RuntimeManager.CreateInstance("event:/SFX/Menu/Menu_Select_Button");
        menuSelect.start();
    }


    // spelas upp när man klickar på något annat än start
    public void PlayOnMenuStart()
    {
        menuPressStart = RuntimeManager.CreateInstance("event:/SFX/Menu/Menu_Press_Start");
        menuPressStart.start();
    }

    // spelas upp när man klickar på start
    public void PlayOnMenuOptions()
    {
        menuPressOptions = RuntimeManager.CreateInstance("event:/SFX/Menu/Menu_Press_Options");
        menuPressOptions.start();
    }

}
