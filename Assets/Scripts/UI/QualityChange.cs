///CREATED BY "John Klingh Ramsin"
///USED FOR "Changes the quality preset"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityChange : MonoBehaviour {

    private Dropdown self;

	// Use this for initialization
	void Start ()
    {
        self = gameObject.GetComponent<Dropdown>();
        self.value = QualitySettings.GetQualityLevel();
        self.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
		
	}
	
    void ValueChangeCheck()
    {

        bool tmp  = Screen.fullScreen;

        switch(self.value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0, tmp);
                break;

            case 1:
                QualitySettings.SetQualityLevel(1, tmp);
                break;

            case 2:
                QualitySettings.SetQualityLevel(2, tmp);
                break;

            case 3:
                QualitySettings.SetQualityLevel(3, tmp);
                break;

            case 4:
                QualitySettings.SetQualityLevel(4, tmp);
                break;

            case 5:
                QualitySettings.SetQualityLevel(5, tmp);
                break;

            case 6:
                QualitySettings.SetQualityLevel(6, tmp);
                break;

            case 7:
                QualitySettings.SetQualityLevel(7, tmp);
                break;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
