///CREATED BY "John Klingh Ramsin"
///USED FOR "setting the resolution"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour {

    private Dropdown self;
    private bool fullscreen;

	// Use this for initialization
	void Start ()
    {
        self = gameObject.GetComponent<Dropdown>();
        fullscreen = Screen.fullScreen;
        self.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void ValueChangeCheck()
    {
        switch (self.value)
        {
            case 0:
                Screen.SetResolution(640, 480, fullscreen);
                break;

            case 1:
                Screen.SetResolution(1024, 768, fullscreen);
                break;

            case 2:
                Screen.SetResolution(1280, 720, fullscreen);
                break;

            case 3:
                Screen.SetResolution(1366, 768, fullscreen);
                break;

            case 4:
                Screen.SetResolution(1368, 768, fullscreen);
                break;

            case 5:
                Screen.SetResolution(1440, 900, fullscreen);
                break;

            case 6:
                Screen.SetResolution(1600, 900, fullscreen);
                break;

            case 7:
                Screen.SetResolution(1600, 1200, fullscreen);
                break;

            case 8:
                Screen.SetResolution(1680, 1050, fullscreen);
                break;

            case 9:
                Screen.SetResolution(1920, 1080, fullscreen);
                break;

            case 10:
                Screen.SetResolution(1920, 1200, fullscreen);
                break;

            case 11:
                Screen.SetResolution(2560, 1440, fullscreen);
                break;

            case 12:
                Screen.SetResolution(2560, 1600, fullscreen);
                break;

        }
    }
}
