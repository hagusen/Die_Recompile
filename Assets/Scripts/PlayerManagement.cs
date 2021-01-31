using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public GameObject player;

    private PlayerMovementScript movement;
    private WeaponManager fire;

    void Start() {
        movement = player.GetComponent<PlayerMovementScript>();
        fire = player.GetComponent<WeaponManager>();
    }

    public void StopPlayerMovement() {
        fire.enabled = false;
        movement.enabled = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<Animator>().SetFloat("horizontalmovement", 0);
        player.GetComponent<Animator>().SetFloat("verticalmovement", 0);
        
        fire.Reset();// move to on level reset

    }

    public void StartPlayerMovement() {
        fire.enabled = true;
        WPHSpawner.ins.ResetWeapons();// move to on level reset
        movement.enabled = true;
    }
}
