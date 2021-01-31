///CREATED BY "John Klingh Ramsin"
///USED FOR "Showing health on the UI"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    public Slider slider;
    public Text text;


    public void Ping(int inData)
    {
        slider.value = inData;
        text.text = inData.ToString() + " / " + slider.maxValue.ToString();
    }

    public void SetMax(int inData)
    {
        slider.maxValue = inData;
        text.text = inData.ToString() + " / " + slider.maxValue.ToString();
    }
}
