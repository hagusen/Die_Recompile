using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigBrain : Enemy, IUseWeapon
{
    public Weapon cannon1;
    public Weapon cannon2;

    protected Rigidbody body;
    protected Animator anim;

    public float turnSpeed;
    public float degreesToShoot;

    public static float randomArea = 5;
    public float activateDistance;
    public float attackDistance;

    public float moveTime;

    public float windUpTime;
    public float windDwnTime;
    public float attackTime;

    protected float attackTimer;
    protected float moveTimer;

    public EnemyAudio eAud;
    public bool isFinalBrain;
    private Text finalText;

    public void TakeKnockback(float power)
    {
        //rb.AddRelativeForce(0, 0, -power);
    }

    public override void DoStart()
    {
        base.DoStart();
        finalText = GameObject.FindGameObjectWithTag("endText").GetComponent<Text>();
        cannon1.managerScript = this;
        cannon2.managerScript = this;

        // make bullet slower (for now)
        cannon1.setAffixes(0.5f);
        cannon2.setAffixes(0.5f);

        anim = GetComponent<Animator>();

        body = GetComponent<Rigidbody>();

        eAud = GetComponent<EnemyAudio>();

        moveTimer = 0;
        attackTimer = 0;

        cannon1.tag = "Hostile";
        cannon2.tag = "Hostile";
    }

    public override void Attack()
    {

        attackTimer += Time.deltaTime;

        if (attackTimer < windUpTime)
        {
            Rotate();
        }

        //Shoot
        if(attackTimer >= windUpTime && attackTimer < attackTime + windUpTime)
        {
            float dist = (transform.position - player.transform.position).magnitude;

            cannon1.Fire(dist);
            cannon2.Fire(dist);

            return;
        }

        else if (attackTimer >= attackTime + windDwnTime + windUpTime)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            anim.SetBool("Shooting", false);
            anim.SetBool("Moving", true);
            state = State.Moving;

            return;
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


                if ((distance <= attackDistance) && (CanSeePlayer()) && rotationDiff() <= degreesToShoot)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Shooting", true);
                    agent.isStopped = true;
                    attackTimer = 0;

                    state = State.Attacking;

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

    public override void Die(DamageType type)
    {
        if (isFinalBrain) {
            finalText.text = "Congratulations! You Beat The Game! \n \nThank You For Playing!";
            GetComponent<Rigidbody>().isKinematic = false;
            ragdoll.SetActive(true);
            base.Die(type);
        } else {
            GetComponent<Rigidbody>().isKinematic = false;
            ragdoll.SetActive(true);
            base.Die(type);
        }
    }
}
