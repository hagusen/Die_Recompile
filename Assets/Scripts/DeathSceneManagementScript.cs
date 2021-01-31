using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathSceneManagementScript : MonoBehaviour
{

    private int deathMessage;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        deathMessage = Random.Range(1, 4);
        switch (deathMessage) {
            case 1:
                 text.text = "Better Luck Next Time I Guess";
            break;

            case 2:
                  text.text = "Do you come here often?";
            break;

            case 3:
                text.text = "Were there too many scary cylinders? Make sure to tell the Designers about it";
            break;

            case 4:
                 text.text = "Just blame that death on a bug or something";
            break;
        }
    }


    public void Retry() {
        SceneManager.LoadScene(0);
    }
}
