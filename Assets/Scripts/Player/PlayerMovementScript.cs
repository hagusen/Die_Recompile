///CREATED BY "Oscar Nordström"
///USED FOR "Moving the player"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementScript : MonoBehaviour {



    public float movementSpeed;

    private int xMovement = 0, zMovement = 0;
    private Vector3 worldMovement = Vector3.zero;

    [Range(0, .3f)]
    [SerializeField]
    private float smoothness;

    private Animator animationController;

    public float xRotateOffset, yRotateOffset, zRotateOffset;

    //NormalMovement makes the movement normalized

    [SerializeField]
    private bool normalMovement;



    [HideInInspector]
    public Rigidbody body;

    private Vector3 movementDirection;
    private float angleRotation;

    private PlayerAudio pAud;
    private Camera mainCamera;


    public float dashTime;
    public float dashSpeed;

    public float cooldown;
    private bool dashOnCd;

    private float tmpTime, tmpDashCd;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main;
        TryGetComponent<PlayerAudio>(out pAud);

        body = gameObject.GetComponent<Rigidbody>();
        animationController = gameObject.GetComponent<Animator>();
        tmpTime = dashTime + 1;

    }



    // Update is called once per frame
    void Update() {
        move();
        rotate();
        updateAnimation();

        if (Input.GetButtonDown("Dash") && !dashOnCd) {
            pAud.PlayOnPlayerDash();
            tmpTime = 0;
            tmpDashCd = 0;
            dashOnCd = true;
        }
        if (dashOnCd) {
            if(tmpDashCd < cooldown) {
                tmpDashCd += Time.deltaTime;
            } else {
                dashOnCd = false;
            }
        }
        if(tmpTime < dashTime) {
            moveDirection = body.velocity;
            tmpTime += Time.deltaTime;
        } else {
            moveDirection = Vector3.zero;
        }
        body.velocity = Vector3.SmoothDamp(body.velocity, moveDirection * dashSpeed, ref worldMovement, smoothness);
       
    }



    private void move() {
        movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (pAud && movementDirection.magnitude != 0) {
            //pAud.PlayOnPlayerMove();
        }

        if (normalMovement) {
            movementDirection.Normalize();
        }

        body.velocity = Vector3.SmoothDamp(body.velocity, movementDirection * movementSpeed, ref worldMovement, smoothness);

    }

    private void rotate() {
        var dir = mainCamera.WorldToScreenPoint(body.position) - new Vector3(Input.mousePosition.x + xRotateOffset, Input.mousePosition.y + yRotateOffset, Input.mousePosition.z + zRotateOffset);
        //Debug.Log(dir);
        if((dir.y < 0 && xRotateOffset > 0) || dir.y > 0 && xRotateOffset < 0) {
            xRotateOffset = -xRotateOffset;
        }

        if ((dir.x > 0 && yRotateOffset > 0) || dir.x < 0 && yRotateOffset < 0) {
            yRotateOffset = -yRotateOffset;
        }
        dir.Normalize();
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angleRotation = angle;
        var rot = Quaternion.AngleAxis(-angle - 90, Vector3.up);
        body.rotation = rot;
    }

    private void updateAnimation() {
        float x = -Mathf.Cos(angleRotation * Mathf.Deg2Rad);
        float z = -Mathf.Sin(angleRotation * Mathf.Deg2Rad);


        //Debug.Log("x = " + x + "  MovementDirection.x = " + movementDirection.x + "  z = " + z + " MovementDirection.z = " + movementDirection.z);
      //  animationController.SetFloat("horizontalmovement",(z * movementDirection.x + x * movementDirection.z));
      //  animationController.SetFloat("verticalmovement", (z * movementDirection.z + + x * movementDirection.x));

        animationController.SetFloat("horizontalmovement", (transform.InverseTransformDirection(body.velocity).x));
        animationController.SetFloat("verticalmovement", (transform.InverseTransformDirection(body.velocity).z));
    }
    public void UpgradeMovementspeed(float modifier) {
        movementSpeed *= modifier;
    }

}