///CREATED BY "John Klingh Ramsin"
///USED FOR "Pauseing the game"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Pause : MonoBehaviour {
    private Canvas active;
    public Canvas menu;


    public WeaponManager wepMan;
    public List<MonoBehaviour> scripts;
    public List<GameObject> objects;

    //tillagd av Axel
    public AudioManager audioManager;

    public Flowchart flow;

    void Start() {
        menu.gameObject.SetActive(false);
        //Time.timeScale = 1;

        //tillagd av Axel
        audioManager = FindObjectOfType<AudioManager>();
    }

    /// <summary>
    /// In this case "Starts and stops time in the scene(used to pause the game)"
    /// </summary>
    public void Stop() {
        if (Time.timeScale == 1) {
            Time.timeScale = 0;

            wepMan.enabled = false;

            foreach (MonoBehaviour i in scripts) {
                i.enabled = false;
            }
            foreach (GameObject i in objects) {
                i.SetActive(false);
            }
        } else if (Time.timeScale == 0) {
            Time.timeScale = 1;

            wepMan.enabled = true;

            foreach (GameObject i in objects) {
                i.SetActive(true);
            }
            foreach (MonoBehaviour i in scripts) {
                i.enabled = true;
            }
        }
    }

    /// <summary>
    /// Changes the active canvas to input parameter
    /// </summary>
    /// <param name="inCanvas">The canvas that should be toggled to</param>
    public void ChangeActive(Canvas inCanvas) {
        if (active != null) {
            active.gameObject.SetActive(false);
        }
        active = inCanvas;
        active.gameObject.SetActive(true);
    }


    public void ExitMenu() {
        active.gameObject.SetActive(false);
        active = null;
        Stop();
        if (audioManager) {

            //tillagd av Axel
            audioManager.inPause = false;
        }
    }

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            if (!flow.GetBooleanVariable("InConversation")) {

                    if (active == null) {
                        Stop();
                        ChangeActive(menu);
                    } else if (active == menu) {
                        ExitMenu();
                    } else {
                        ChangeActive(menu);
                    }
                if (Time.timeScale == 0) {
                    if ((Input.GetAxisRaw("Mouse X") != 0) || (Input.GetAxisRaw("Mouse Y") != 0)) {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }
        }
    }
}
