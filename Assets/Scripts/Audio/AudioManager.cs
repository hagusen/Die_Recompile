using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public StudioEventEmitter masterMusic;
    public StudioEventEmitter labMusic;
    public StudioEventEmitter menuMusic;
    public StudioEventEmitter pauseAlt;
    public StudioEventEmitter deathStinger;
    public PlayerStats playerStats;
    public ElevatorScript elevatorScript;
    public Pause pauseMenu;
    public EvilPlayer evilPlayer;

    private GameObject[] audioManagers;

    [HideInInspector]
    public bool toSwitch;

    [HideInInspector]
    public bool inPause;

    [HideInInspector]
    public bool inMainMenu;

    public float rnd;
    public float r;
    public int randIndex;

    public int counter = 1;
    public int firstRand = 1;
    [HideInInspector]
    public bool reRandAllowed = false;
    private bool isDead;

    [HideInInspector]
    //public ElevatorScript[] elevatorS;

    //public int countElevator = 0;

    //public ElevatorScript activeElevator;

    int doOnce = 1;

    //[HideInInspector]
    //public AudioManagerMenu audioManagerMenu;

    private void Awake() {

        //if (SceneManager.GetActiveScene().name == "Menu")
        //{
        //    inMainMenu = true;
        //}

        //if (inMainMenu == false)
        //{
        evilPlayer = FindObjectOfType<EvilPlayer>();
        //}
        //audioManagerMenu = FindObjectOfType<AudioManagerMenu>();


        playerStats = FindObjectOfType<PlayerStats>();
        pauseMenu = FindObjectOfType<Pause>();
        //audioManager = FindObjectOfType<AudioManager>();
        deathStinger = GameObject.FindGameObjectWithTag("DeathStinger").GetComponent<StudioEventEmitter>();
        masterMusic = GameObject.FindGameObjectWithTag("MasterMusic").GetComponent<StudioEventEmitter>();
        labMusic = GameObject.FindGameObjectWithTag("LabMusic").GetComponent<StudioEventEmitter>();
        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<StudioEventEmitter>();
        pauseAlt = GameObject.FindGameObjectWithTag("MusicPauseAlt").GetComponent<StudioEventEmitter>();

    }

    // Start is called before the first frame update
    void Start() {
        if (elevatorScript == true) {
            if (elevatorScript.inLab == true) {
                toSwitch = true;
            } else {
                toSwitch = false;
            }
        }

        inPause = false;

        audioManagers = GameObject.FindGameObjectsWithTag("AudioManager");
        int i = 0;
        foreach (GameObject audio in audioManagers) {
            if (i > 0) {
                Destroy(audio);
            }
            i++;
        }
        DontDestroyOnLoad(this);

        //DontDestroyOnLoad(audioManager);
        //DontDestroyOnLoad(pauseAlt);
        //DontDestroyOnLoad(labMusic);
        //DontDestroyOnLoad(masterMusic);
        //DontDestroyOnLoad(menuMusic);
    }

    // Update is called once per frame
    void Update() {

        if (SceneManager.GetActiveScene().name == "Menu") {
            if (pauseAlt)
                pauseAlt.Stop();
            if (labMusic)
                labMusic.Stop();
            if (masterMusic)
                masterMusic.Stop();



            inPause = false;

            //if (masterMusic.IsPlaying() == true || labMusic.IsPlaying() == true || pauseAlt.IsPlaying() == true)
            //{
            //masterMusic = GameObject.FindGameObjectWithTag("MasterMusic").GetComponent<StudioEventEmitter>();
            //labMusic = GameObject.FindGameObjectWithTag("LabMusic").GetComponent<StudioEventEmitter>();
            //pauseAlt = GameObject.FindGameObjectWithTag("MusicPauseAlt").GetComponent<StudioEventEmitter>();

            //pauseAlt.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //labMusic.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //masterMusic.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


            //}


            if (menuMusic != null) {
                if (menuMusic.IsPlaying() == false) {
                    menuMusic.Play();
                }

            }
        } else if (SceneManager.GetActiveScene().name == "GamePrototype 1") {
            inMainMenu = false;

            if (Input.GetButtonDown("Pause")) {
                if (inPause == false) {
                    inPause = true;
                } else if (inPause == true) {
                    inPause = false;
                }

            }

            Debug.Log("playerhp: " + playerStats.stats.health);
            if (playerStats.stats.health <= 0 && isDead == false) {
                deathStinger.Play();
                Debug.Log("ds started");

                masterMusic.Stop();
                pauseAlt.Stop();
                if (menuMusic != null) {
                    menuMusic.Stop();
                }
                labMusic.Stop();

      
                isDead = true;
            }
            if (isDead == true && playerStats.stats.health > 0) {
                isDead = false;
                toSwitch = true;
                elevatorScript.inLab = true;
            }

            /*if(reRandAllowed == true)
            {

                elevatorS = FindObjectsOfType<ElevatorScript>();
                Debug.Log("elevators: " + elevatorS);
                elevatorS[countElevator] = elevatorScript;
               
                if (counter == firstRand)
                {
                    rnd = Random.value;
                    counter++;
                    Debug.Log("Actionrandomizer value:" + rnd);

                }
                else if (elevatorScript.reRandomMusic == true)
                {
                    Debug.Log("have entered rerandomizer");
                    ReRandomizer();
                    SwitchElevator();
                }
                
            }*/




            Debug.Log("inlab: " + elevatorScript.inLab + "  toswitch: " + toSwitch);
            if (elevatorScript.inLab == true && toSwitch == true) {
                //UpdateImports();
                toSwitch = false;
                r = Random.value;
                Debug.Log("lab random: " + r);

                //menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<StudioEventEmitter>();

                deathStinger.Stop();
                pauseAlt.Stop();
                if (menuMusic != null) {
                    menuMusic.Stop();
                    menuMusic.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                }
                masterMusic.Stop();
                //masterMusic.SetParameter("GameOver", 0);
                Debug.Log("param killed");

                labMusic.Play();
                labMusic.SetParameter("LabRandomizer", r);

                Debug.Log("hello we in lab");
            } else if (elevatorScript.inLab == false && toSwitch == false) {
                //UpdateImports();
                toSwitch = true;

                Debug.Log("we in field");

                //Debug.Log("Actionrandomizer value:" + r);
                masterMusic.SetParameter("cloneFight", 0);

                deathStinger.Stop();
                pauseAlt.Stop();
                if (menuMusic != null) {
                    menuMusic.Stop();
                }
                labMusic.Stop();

                masterMusic.Play();
                masterMusic.SetParameter("ActionRandomizer", rnd);
                reRandAllowed = true;
                //elevatorScript.reRandomMusic = false;
            }


            if (inPause == true) {
                Debug.Log("paused play");
                pauseAlt.Play();
            } else {
                pauseAlt.Stop();
            }

            //if (SceneManager.GetActiveScene().name == "Menu")
            //{
            //  pauseAlt.Stop();
            //labMusic.Stop();
            //masterMusic.Stop();
            //}
        }



    }

    /*public void SwitchElevator() { 
    
        countElevator++;
    }*/

    public void ReRandomizer() {
        int i = Random.Range(0, 3);
        if(i == randIndex)
        {
            i = (i + 1) % 3;
        }

        randIndex = i;
        rnd = i / 2f;

        /*float checkNmbr = rnd;
        Debug.Log("checkNmbr " + checkNmbr);
        rnd = Random.value;
        Debug.Log("rnd now " + rnd);

    RandomCheck:
        //Debug.Log("retrying rerandom");
        if (checkNmbr >= 0.5 && rnd >= 0.5) {
            rnd = Random.value;
            Debug.Log("too big, do over");
            goto RandomCheck;
        } else if (checkNmbr <= 0.5 && rnd <= 0.5) {
            rnd = Random.value;
            Debug.Log("too small, do over");
            goto RandomCheck;
        }
        */
        Debug.Log("Actionrandomizer value:" + rnd);
        masterMusic.Stop();
        masterMusic.Play();
        masterMusic.SetParameter("ActionRandomizer", rnd);
        Debug.Log("Rerandomized");
        //elevatorScript.reRandomMusic = false;
        Debug.Log("rerandom now: " + elevatorScript.reRandomMusic);
    }
    public void UpdateImports() {

        evilPlayer = FindObjectOfType<EvilPlayer>();

        //audioManagerMenu = FindObjectOfType<AudioManagerMenu>();
        playerStats = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
        elevatorScript = GameObject.FindGameObjectWithTag("MainElevator").GetComponent<ElevatorScript>();
        pauseMenu = FindObjectOfType<Pause>();
        //audioManager = FindObjectOfType<AudioManager>();
        deathStinger = GameObject.FindGameObjectWithTag("DeathStinger").GetComponent<StudioEventEmitter>();
        masterMusic = GameObject.FindGameObjectWithTag("MasterMusic").GetComponent<StudioEventEmitter>();
        labMusic = GameObject.FindGameObjectWithTag("LabMusic").GetComponent<StudioEventEmitter>();
        if (menuMusic != null) {
            menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<StudioEventEmitter>();
        }
        pauseAlt = GameObject.FindGameObjectWithTag("MusicPauseAlt").GetComponent<StudioEventEmitter>();
    }

    public void EvilPlayerMusic(bool ePActivated)
    {

            if (ePActivated == true)
            {
                masterMusic.SetParameter("cloneFight", 1);

                Debug.Log("clonefightparam activated");
            }
            else if (ePActivated == false)
            {
                masterMusic.SetParameter("cloneFight", 0);

                Debug.Log("clonefightparam deactivated");

            }
            else
            {
                Debug.Log("isActivated not true :(");
            }
    } 

    public void NewScene() {
        UpdateImports();
        toSwitch = true;
    }

    public void MenuSceneEnter() {
        StopAllMusic();
    }

    public void StopAllMusic() {
        // pauseAlt.Stop();
        // masterMusic.Stop();
        // labMusic.Stop();
        // if (menuMusic != null) {
        //     menuMusic.Stop();
        // }

    }

}
