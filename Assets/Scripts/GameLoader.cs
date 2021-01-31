using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {


    public Slider slider;
    public int sceneIndex;
    private int max = 0;
    private int curr = 0;
    private float progress;

    void Start()
    {
        max = 1;
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync (int index)
    {
        Cursor.visible = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync("GamePrototype 1", LoadSceneMode.Additive);
        //operation.allowSceneActivation = false;
        Time.timeScale = 0;

        while (!operation.isDone)
        {
            yield return null;
        }

        if (operation.isDone)
        {
            yield return StartCoroutine(GameObject.FindGameObjectWithTag("WorldGen").GetComponent<WorldGeneration>().DoStart(this));
            Time.timeScale = 1;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("GamePrototype 1"));
            SceneManager.MergeScenes(SceneManager.GetSceneAt(0), SceneManager.GetSceneAt(1));
            Cursor.visible = true;
            Destroy(gameObject);
        }


        yield return null;
    }

    public void SetRoomCount(int inData)
    {
        max = inData;
        slider.maxValue = max;
    }
    public void IncreaseCount()
    {
        curr++;
        slider.value = curr;
    }
}
