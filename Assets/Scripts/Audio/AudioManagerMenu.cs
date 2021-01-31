using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class AudioManagerMenu : MonoBehaviour
{
    public StudioEventEmitter masterMusic;
    public StudioEventEmitter labMusic;
    public StudioEventEmitter menuMusic;
    public StudioEventEmitter pauseAlt;
    public AudioManager audioManager;

    private void Awake()
    {

    }

    private void Start()
    {
        masterMusic = GameObject.FindGameObjectWithTag("MasterMusic").GetComponent<StudioEventEmitter>();
        labMusic = GameObject.FindGameObjectWithTag("LabMusic").GetComponent<StudioEventEmitter>();
        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<StudioEventEmitter>();
        pauseAlt = GameObject.FindGameObjectWithTag("MusicPauseAlt").GetComponent<StudioEventEmitter>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (menuMusic == false)
            {
                menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<StudioEventEmitter>();

                Debug.Log("found it");
            }

            //audioManager.masterMusic.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //audioManager.labMusic.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //audioManager.pauseAlt.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            if (menuMusic.IsPlaying() == false)
            {
                menuMusic.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "GamePrototype 1")
        {
            menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<StudioEventEmitter>();
            
            menuMusic.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            Debug.Log("stopped menumusic");
        }

    }
}
