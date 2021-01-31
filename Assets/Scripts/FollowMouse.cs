using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public static int zAxisPos = 0; //Static because other scripts may need to get this.
    private float yAxisPos = -5.23f;
    public float xAxisBoundry = 7.5f;
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = new Vector3(Input.GetAxis("Mouse X"), 0, 0);


        Debug.Log(mouse);
        Debug.Log("screentoworld " + Camera.main.ScreenToWorldPoint(mouse));

        this.transform.position = new Vector3(mouse.x, yAxisPos, zAxisPos);
    }
}
