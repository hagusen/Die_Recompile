using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public int floorIndex;

    public int floorDestination;

    public bool isEntrance;

    public bool inLab;

    //Axel lade till denna
    public bool reRandomMusic;

    public WorldGeneration world;
    public EnemySpawner spawner;


    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {

            if (isEntrance) {

                GetComponent<ElevatorAudio>().PlayOnElevatorOpen();
                inLab = false;

                //Axel shenanigans
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                if (audioManager) {
                    audioManager.ReRandomizer();
                }
                //reRandomMusic = true;
                //Debug.Log("rerandom: " + reRandomMusic);

                if(floorIndex >= 0)
                {
                    spawner.floors[floorIndex].RemoveEnemies();
                    spawner.floors[floorIndex].gameObject.SetActive(false);
                }
            
                spawner.floors[floorDestination].gameObject.SetActive(true);

                spawner.SpawnEnemies(floorDestination);

                spawner.BakeFloor(floorDestination);

                if (other.gameObject.GetComponent<PlayerStats>())
                {
                    other.gameObject.GetComponent<PlayerStats>().floor = floorDestination;
                }
            
                other.gameObject.transform.position = new Vector3((floorDestination) * world.GetComponent<WorldGeneration>().floorXOffset + 48, 1, 10);
            }
        }
    }

}
