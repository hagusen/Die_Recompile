using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHud : MonoBehaviour
{
    public List<GameObject> objectsToToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleObjects();
        }
    }

    public void ToggleObjects()
    {
        foreach(GameObject i in objectsToToggle)
        {
            i.SetActive(!i.activeSelf);
        }
    }

}
