using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBullet : Bullet
{


    //public BulletInfo info;
    public BulletExplosion explosionPrefab;

    private Rigidbody rb;

    public float speedMultiplier = 1;

    Vector3 prevPos;


    public void SetAffixes() {

    }


    void OnEnable() {
        /*
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.drag = info.drag;
        rb.AddRelativeForce(0, 0, info.speed * speedMultiplier);
        */
        //Destroy(gameObject, info.lifetime);//change later
        Invoke("RemoveSelf", info.lifetime);
        x = 1;
        prevPos = transform.position;
    }
    RaycastHit[] hits;
    Ray ray;
    RaycastHit hit;
    // Update is called once per frame

    float x = 1;
     void FixedUpdate()
    {


        transform.Translate(0, 0, info.speed * Time.deltaTime * x);

        if (Physics.Raycast(prevPos, (transform.position - prevPos).normalized, out hit, info.speed * Time.deltaTime)) {

            Vector3 dir = Vector3.Reflect(transform.forward, hit.normal);
            var rot = 90 - Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            x = x * 2;
            //rb.velocity = transform.forward * rb.velocity.magnitude;
        }
        //hits = Physics.RaycastAll(prevPos, (transform.position - prevPos).normalized, info.speed * Time.deltaTime);

        //foreach (var item in hits) {
            //Debug.Log(item);
        //}



        prevPos = transform.position;

    }
}
