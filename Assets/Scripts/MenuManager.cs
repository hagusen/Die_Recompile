using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    private GameObject audio;
    private void Awake() {
        audio = GameObject.FindGameObjectWithTag("AudioManager");
        audio.GetComponent<AudioManager>().MenuSceneEnter();
    }
}
