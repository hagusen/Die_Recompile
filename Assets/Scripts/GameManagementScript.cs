using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementScript : MonoBehaviour {


    public void ChangeScene(int sceneIndex) {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneIndex));
        StartCoroutine(SceneLoading(sceneIndex));
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }


    IEnumerator SceneLoading(int sceneIndex) {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!asyncLoad.isDone) {
            yield return null;
        }

    }
}
