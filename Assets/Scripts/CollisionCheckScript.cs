using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class CollisionCheckScript : MonoBehaviour
{

    public Flowchart conversationChart;



    private void OnTriggerEnter(Collider other) {
        switch (gameObject.name) {
            case "Rita":
            conversationChart.ExecuteBlock("Rita on");
            break;



            case "Cornelia":
            conversationChart.ExecuteBlock("Cornelia on");
            break;


            case "Thomas":
            conversationChart.ExecuteBlock("Thomas on");
            break;
        }
    }


    private void OnTriggerExit(Collider other) {
        switch (gameObject.name) {
            case "Rita":
            conversationChart.ExecuteBlock("Rita off");
            break;



            case "Cornelia":
            conversationChart.ExecuteBlock("Cornelia off");
            break;


            case "Thomas":
            conversationChart.ExecuteBlock("Thomas off");
            break;
        }
    }
}
