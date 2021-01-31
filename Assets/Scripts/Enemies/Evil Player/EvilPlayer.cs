using Fungus;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting;
using UnityEngine;

public class EvilPlayer : Enemy, IUseWeapon
{
    public Weapon mainWeapon;
    public GameObject mainWPos;

    protected Rigidbody body;
    protected Animator anim;

    public BulletInfo bullet;

    private float simpleTimer;

    public float turnSpeed;
    public float degreesToShoot;


    public static float randomArea = 5;
    public float activateDistance;
    public float attackDistance;

    public float attackCooldown;

    public float moveTime;

    public bool isActivated;

    protected float attackTimer;
    protected float attackCdTimer;
    protected float moveTimer;

    public void TakeKnockback(float power) {
        //rb.AddRelativeForce(0, 0, -power);
    }

    public override void DoStart()
    {
        base.DoStart();

        mainWeapon = Instantiate(player.GetComponent<PlayerStats>().previousWeapon.gameObject).GetComponent<Weapon>();
        //mainWeapon = player.GetComponent<PlayerStats>().previousWeapon;
        mainWeapon.Activate();
        mainWeapon.enabled = true;
        mainWeapon.transform.SetParent(mainWPos.transform, false);
        mainWeapon.transform.localPosition = new Vector3(0, 0, 0);
        mainWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
        //mainWeapon.transform.localScale *= 0.01f;
        if (mainWeapon.info.weaponType == WeaponType.Standard)
        {
            //Debug.LogError("This will get an error in build (you can't change an scriptable object)");
            mainWeapon.OverrideBullet(bullet);
        }


        isActivated = false;

        mainWeapon.managerScript = this;

        mainWeapon.setAffixes(0.5f); // make bullet slower (for now)

        anim = GetComponent<Animator>();

        body = GetComponent<Rigidbody>();

        mainWeapon.tag = "Hostile";

        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        renderer.material.color = Color.red;
  
    }

    public override void Attack()
    {
        state = State.Moving;
    }

    public override void Move()
    {
        if (player != null)
        {
            attackCdTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;

            if (mainWeapon)
            {
                var dist = (transform.position - player.transform.position).magnitude;

                agent.transform.LookAt(player.transform.position);

                if (rotationDiff() < degreesToShoot)
                {
                    anim.SetBool("shooting", true);
                    mainWeapon.Fire(dist);
                    state = State.Moving;
                }
            }

            float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

            if (distance >= activateDistance)
            {
                state = State.Idle;
                isActivated = false;
                return;
            }

            if (moveTimer >= moveTime)
            {
                if (distance >= attackDistance + 1)
                {
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    NewPosOnCircle();
                }
                moveTimer = 0;
            }
            if(!CanSeePlayer())
            {
                NewPosOnCircle();
            }
        }
        UpdateAnimation();
    }

    public override void Idle()
    {
        if (player != null)
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < activateDistance)
            {
                agent.enabled = true;
                state = State.Moving;
                isActivated = true;

                //axel shenanigans
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                if (audioManager) {
                    audioManager.EvilPlayerMusic(isActivated);

                }
            }
        }
    }

    private void UpdateAnimation()
    {
        anim.SetFloat("horizontalmovement", (transform.InverseTransformDirection(agent.velocity).x));
        anim.SetFloat("verticalmovement", (transform.InverseTransformDirection(agent.velocity).z));
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

    public override void Die(DamageType type)
    {
        base.Die(type);
        isActivated = false;
        //axel shenanigans
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        audioManager.EvilPlayerMusic(isActivated);
    }

    public void NewPosOnCircle()
    {
        Vector3 tmp = Random.insideUnitCircle.normalized * attackDistance;

        agent.SetDestination(player.transform.position + Random.onUnitSphere * attackDistance);
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
