///CREATED BY "John Klingh Ramsin"
///USED FOR "The melee enemy"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    [Header("MeleeEnemy")]
    public float attackDistance;
    public float activateDistance;
    public float goodMoveDistance;
    public static float randomArea = 5;

    public float attackTime;
    public float moveTime;
    public int damage;

    public float attackStart;
    public float attackCooldown;

    protected float attackTimer;
    protected float attackCdTimer;
    protected float moveTimer;

    protected Animator anim;

    public EnemyAudio eAud;

    public override void DoStart()
    {
        base.DoStart();
        attackTimer = 0;
        moveTimer = 0;
        anim = GetComponent<Animator>();
        eAud = GetComponent<EnemyAudio>();
    }

    public override void Idle()
    {
        if (player != null)
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < activateDistance)
            {
                agent.enabled = true;
                state = State.Moving;
                anim.SetBool("Moving", true);
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
                state = State.Idle;
                anim.SetBool("Moving", false);
                agent.SetDestination(transform.position);

                return;
            }

            if ((distance <= attackDistance) && (attackCdTimer >= attackCooldown))
            {
                agent.isStopped = true;
                state = State.Attacking;
                attackTimer = 0;
                attackCdTimer = 0;
                if (GetComponent<EnemyAudio>()) {
                    GetComponent<EnemyAudio>().PlayOnEnemyAgro();
                }
                return;
            }

            if (moveTimer >= moveTime)
            {
                if (distance <= goodMoveDistance)
                {
                    agent.SetDestination(player.transform.position);
                    eAud.PlayOnEnemyAgro();
                }
                else
                agent.SetDestination(player.transform.position + Random.insideUnitSphere * randomArea);

                moveTimer = 0;
            }
        }
    }

    public override void Attack()
    {
        

        if (attackTimer >= attackTime)
        {
            state = State.Moving;
            agent.isStopped = false;
            if (eAud)
            {
                eAud.PlayOnEnemyAttack();
            }
        }
    }

    private new void Update()
    {
        attackCdTimer += Time.deltaTime;
        base.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (attackCdTimer >= attackCooldown)
            {
                other.gameObject.GetComponent<PlayerStats>().Damage(damage, DamageType.Normal);
                eAud.PlayOnEnemyAttack();
                attackCdTimer = 0;
            }
        }
    }
}