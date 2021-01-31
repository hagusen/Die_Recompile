using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAMPLEAI : MonoBehaviour, IUseWeapon {


    public Weapon mainWeapon;
    public GameObject mainWPos;

    private Rigidbody rb;


    private float simpleTimer;


    public void TakeKnockback(float power) {
        //rb.AddRelativeForce(0, 0, -power);
    }


    // Start is called before the first frame update
    void Start() {

        
        mainWeapon.managerScript = this;

        mainWeapon.setAffixes(0.5f); // make bullet slower (for now)



        rb = GetComponent<Rigidbody>();
        if (gameObject.GetComponent<TagsScript>().HaveTag("Hostile")) {
            mainWeapon.tag = "Hostile";

        }

        
       

    }




    // Update is called once per frame
    void Update() {


        if (mainWeapon) {

            var target = GameObject.FindGameObjectWithTag("Player");

            var dist = (transform.position - target.transform.position).magnitude;

            mainWeapon.Aim(dist);



            if (simpleTimer > 2) {
                mainWeapon.Fire(dist);
                simpleTimer = 0;
            }
        }

        if (simpleTimer <= 2) {
            simpleTimer += Time.deltaTime;
        }

    }
}
