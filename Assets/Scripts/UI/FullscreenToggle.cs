///CREATED BY "John Klingh Ramsin"
///USED FOR "Toggles fullscreen"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour {

    private Toggle self;

	// Use this for initialization
	void Start ()
    {
        self = gameObject.GetComponent<Toggle>();

        if(Screen.fullScreen == true)
        {
            self.isOn = true;
        }
        else
        {
            self.isOn = false;
        }

        self.onValueChanged.AddListener(delegate { ValueChangeCheck(); });


    }
	
    void ValueChangeCheck()
    {
        if(self.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
