///CREATED BY "John Klingh Ramsin"
///USED FOR "Handles the main menu"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private Canvas active;
    public Canvas menu;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        active = menu;
        active.gameObject.SetActive(true);
    }

    public void ChangeActive(Canvas meme)
    {
        if (active != null)
        {
            active.gameObject.SetActive(false);
        }
        active = meme;
        active.gameObject.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (active != menu)
            {
                ChangeActive(menu);
            }
            else
            {
                Quit();
            }
        }
        if (Time.timeScale == 0)
        {
            if ((Input.GetAxisRaw("Mouse X") != 0) || (Input.GetAxisRaw("Mouse Y") != 0))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
