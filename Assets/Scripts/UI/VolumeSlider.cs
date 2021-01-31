///CREATED BY "John Klingh Ramsin"
///USED FOR "Setting the volume"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    private Slider slider;
    public InputField input;

   

    // Use this for initialization
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();


        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    void ValueChangeCheck()
    {
        float meme = slider.value * 100;
        input.text = meme.ToString();
        AudioListener.volume = slider.value;
    }
}
