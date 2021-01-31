using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//CREATED BY "John Boman"
//USED FOR "Holding weapons in the level"

public class WeaponHolderPoint : MonoBehaviour {

    public Weapon weaponPrefab;
    public GameObject weaponPosition;

    public GameObject textLabelPos;

    public string weaponName;

    public bool giveAmmo = true;





    void Awake() {
        if (weaponPrefab) {
            weaponName = weaponPrefab.GetComponent<Weapon>().info.weaponName;
        }
        else {  Debug.LogError("I have no Weapon attached ");    }
    }



    public void TakeWeapon(Weapon weapon, out Weapon weaponOut, out bool empty, out bool haveAmmo) { //optional weapon else null

        if (!weaponPrefab) {
            Debug.LogError("I have no Weapon attached " + Faces.GetFace(faceType.Sad, 1));
            weaponOut = weapon;
            empty = true;
            haveAmmo = false;
            Disable();
            return;
        }
        weaponOut = weaponPrefab;

        if (weapon) {
            weaponPrefab = weapon;
            weaponPrefab.Deactivate();
            weaponName = weaponPrefab.info.weaponName;
            weaponPrefab.transform.SetParent(weaponPosition.transform, false);
            empty = false;
            haveAmmo = giveAmmo;
            giveAmmo = false;
            TextLabelController.OnTextLabelRemoved(textLabelPos);
            TextLabelController.OnTextLabelAdded(textLabelPos, weaponName); // ???
        }
        else {
            //destroy / disable
            empty = true;
            haveAmmo = giveAmmo;
            giveAmmo = false;
            Disable();
        }



    }


    void Disable() {
        gameObject.SetActive(false);
        TextLabelController.OnTextLabelRemoved(textLabelPos);
    }

    void OnTriggerEnter(Collider other) { // Add empty func

        //Debug.Log(other.tag);
        if (other.CompareTag("Player")) {


            TextLabelController.OnTextLabelAdded(textLabelPos, weaponName);
        }

    }
    void OnTriggerExit(Collider other) {

        //Debug.Log(other.tag);
        if (other.CompareTag("Player")) {
            if (other.GetComponent<WeaponManager>().weaponHoldPoint == this) {
                other.GetComponent<WeaponManager>().weaponHoldPoint = null;
            }

            TextLabelController.OnTextLabelRemoved(textLabelPos);

        }

    }




}
