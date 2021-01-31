///CREATED BY "John Klingh Ramsin"
///USED FOR "Holding important enemy related variables"

using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Declare any public variables that you want to be able 
    // to access throughout your scene
    public GameObject player;
    public PlayerStats playerStats;
    public EnemyBloodPool enemyBloodPool;
    public EnemySpawner enemySpawner;
  //  public NavMeshBake navMesh;
    public List<NavMeshBake> navMeshes;
    public Text cashText;
    public Flowchart flowchart;
    public AudioManager audio;
    public static Manager Instance { get; private set; } // static singleton

    void Awake() {
        audio = GameObject.FindObjectOfType<AudioManager>();
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        // Cache references to all desired variables
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerStats = player.GetComponent<PlayerStats>();
        if (audio != null)
        {
            audio.NewScene();
        }
    }
}