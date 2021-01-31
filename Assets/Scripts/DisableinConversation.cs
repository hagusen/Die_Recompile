using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableinConversation : MonoBehaviour
{
    public List<MonoBehaviour> scripts;
    public List<GameObject> objects;

    public void SetActive(bool newValue)
    {
        foreach(MonoBehaviour i in scripts)
        {
            i.enabled = newValue;
        }
        foreach (GameObject i in objects)
        {
            i.SetActive(newValue);
        }
    }
}
