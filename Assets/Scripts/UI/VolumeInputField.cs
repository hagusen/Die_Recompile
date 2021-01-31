///CREATED BY "John Klingh Ramsin"
///USED FOR "Setting the volume using an input"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeInputField : MonoBehaviour {

    public Slider slider;
    private InputField input;



    // Use this for initialization
    void Start ()
    {
        input = gameObject.GetComponent<InputField>();

        float meme = 100;//AudioListener.volume * 100;
        AudioListener.volume = 1;
        print(meme);
        input.text = meme.ToString();
        input.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueChangeCheck();

    }


    void ValueChangeCheck()
    {
        if (input.text != "" && float.Parse(input.text) <= 100)
        {
            slider.value = float.Parse(input.text) / 100;
            AudioListener.volume = float.Parse(input.text) / 100;
        }
        else
        {
            input.text = "0";
            slider.value = 0;
            AudioListener.volume = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
