///CREATED BY "Oscar Nordström"
///USED FOR "Moving the camera"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Man väljer ett objekt som detta objekt följder med en viss offset
/// </summary>
public class CameraMovement : MonoBehaviour {

    public Transform followingObject;

    public Vector3 offset = new Vector3(0, 15, -5);
    public float cameraTilt;
    public float Damping = 7;
    [Range(0,1)]
    public float xfollowPercentage = 0.2f;
    [Range(0, 1)]
    public float yfollowPercentage = 0.2f;
    public float treshold = 40;


    private Transform objPos;

    private Camera mainCamera;
    private Plane plane = new Plane(Vector3.up, 0f);

    void Start() {

        mainCamera = Camera.main;

        objPos = followingObject;

        if (!objPos) { // If no target defined then use player
            objPos = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    // Update is called once per frame
    void FixedUpdate() {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        /*
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        */
        Vector3 pos = objPos.position;
        float distToPlane;
        if (plane.Raycast(ray, out distToPlane)) {
            pos = ray.GetPoint(distToPlane);

        }

        var lookDir = pos - objPos.position;



        transform.rotation = Quaternion.Euler(cameraTilt, 0, 0);


        if (lookDir.magnitude < treshold) {

            transform.position = Vector3.Lerp(transform.position, objPos.position + new Vector3(lookDir.x * xfollowPercentage,0 , lookDir.z * yfollowPercentage) + offset, Time.deltaTime * Damping);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, objPos.position + lookDir.normalized * treshold * xfollowPercentage + offset, Time.deltaTime * Damping);
        }



    }


}




