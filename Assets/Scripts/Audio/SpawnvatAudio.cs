using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SpawnvatAudio : MonoBehaviour
{
    public void PlayOnSpawnOpen (string path)
    {
        RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    public void PlayOnSpawnCloses (string path)
    {
        RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
