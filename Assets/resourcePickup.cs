using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class resourcePickup : MonoBehaviour {

    public int minResource, maxResource;
    private Flowchart flow;

    void Start() {
        GameObject obj = GameObject.FindGameObjectWithTag("Flowchart");
        flow = obj.GetComponent<Flowchart>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<TagsScript>().HaveTag("Player")) {
            int tmp = flow.GetIntegerVariable("Cash");
            flow.SetIntegerVariable("Cash", tmp + Random.Range(minResource, maxResource + 1));

            Manager.Instance.cashText.text = flow.GetIntegerVariable("Cash").ToString();

            Destroy(this);
        }
    }
}
