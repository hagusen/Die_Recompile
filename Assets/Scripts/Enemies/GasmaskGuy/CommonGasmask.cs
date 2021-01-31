using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGasmask : Enemy, IUseWeapon
{
    public Weapon mainWeapon;
    public GameObject mainWPos;

    protected Rigidbody body;
    protected Animator anim;

    public float turnSpeed;
    public float degreesToShoot;


    public static float randomArea = 5;
    public float activateDistance;
    public float attackDistance;

    public float moveTime;
    public float waitTime;

    protected float moveTimer;
    protected float waitTimer;

    public GasmaskAudio gAud;

    public void TakeKnockback(float power)
    {
        //rb.AddRelativeForce(0, 0, -power);
    }



    public override void DoStart()
    {
        base.DoStart();

        mainWeapon.managerScript = this;

        mainWeapon.setAffixes(0.5f); // make bullet slower (for now)

        anim = GetComponent<Animator>();

        body = GetComponent<Rigidbody>();

        moveTimer = 0;
        waitTimer = 0;

        mainWeapon.tag = "Hostile";

        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        renderer.material.color = Color.red;

        gAud = GetComponent<GasmaskAudio>();

    }

    public override void Attack()
    {
        Rotate();
        waitTimer += Time.deltaTime;
        if (waitTimer >= waitTime)
        {

            if ((Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance) || (!CanSeePlayer()))
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                anim.SetBool("Moving", true);
                state = State.Moving;

                return;
            }

            if (mainWeapon)
            {

                var target = GameObject.FindGameObjectWithTag("Player");

                var dist = (transform.position - target.transform.position).magnitude;

                //mainWeapon.Aim(dist);



                if (rotationDiff() < degreesToShoot)
                {
                    if (mainWeapon.Fire(dist))
                    {
                        anim.SetTrigger("Shoot");
                    }
                }
            }
        }
    }

    public override void Move()
    {
        if (player != null)
        {
            moveTimer += Time.deltaTime;

            float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        
            if (distance >= activateDistance)
            {
                anim.SetBool("Moving", false);
                state = State.Idle;
                return;
            }


            if (moveTimer >= moveTime)
            {
                moveTimer = 0;
                agent.SetDestination(player.transform.position);


                if ((distance <= attackDistance) && (CanSeePlayer()))
                {
                    anim.SetBool("Moving", false);
                    agent.isStopped = true;
                    waitTimer = 0;
                    state = State.Attacking;
                    gAud.PlayGasmaskAgro();
                    return;
                }

                
            }
        }
    }

    public override void Idle()
    {
        if (player != null)
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < activateDistance)
            {
                agent.enabled = true;
                anim.SetBool("Moving", true);
                state = State.Moving;
            }
        }
    }

    protected void Rotate()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = Manager.Instance.player.transform.position - transform.position;

        targetDirection.y = transform.position.y;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    protected float rotationDiff()
    {
        Vector3 targetDirection = (Manager.Instance.player.transform.position - transform.position).normalized;
        Vector3 currentDirection = transform.forward;

        float degrees = Vector3.Angle(targetDirection, currentDirection);

        return degrees;
    }

    protected bool CanSeePlayer()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        int tmp = 1 << 10;
        layerMask = layerMask | tmp;

        RaycastHit hit;
        Vector3 direction = Manager.Instance.player.transform.position - transform.position;

        Physics.Raycast(transform.position, direction, out hit, 500, layerMask);

        // Does the ray intersect any objects in the player or wall layer
        if (hit.transform != null)
        {
            if ((hit.transform.gameObject.layer) == 8)
            {
                Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, direction * 500, Color.white);
                return false;
            }
        }
        return false;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        //Debug.Log(agent.isStopped);
    }

    /*
    protected void GrabWeapon()
    {
        bool haveAmmo;
        bool isEmpty;

        weaponHoldPoint.TakeWeapon(mainWeapon, out mainWeapon, out isEmpty, out haveAmmo);

        mainWeapon.managerScript = this;
        mainWeapon.transform.SetParent(mainWPos.transform, false);
        mainWeapon.Activate();

        if (mainWeapon)
        {
            OnWeaponTypeChanged(mainWeapon.bulletInfo.type);
        }

        if (GetComponent<TagsScript>().HaveTag("Friendly"))
        {
            mainWeapon.tag = "Friendly";
        }
    }
    */


}
