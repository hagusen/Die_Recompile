using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomber : MeleeEnemy
{
    [Header("Bomber")]
    public GameObject explosionPrefab;

    public override void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackStart)
        {
            Die(DamageType.Normal); // change to bomber specific?
        }
    }

    public override void Die(DamageType type)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        base.Die(type);
    }

    public override void Move()
    {
        base.Move();
        if ((Vector3.Distance(gameObject.transform.position, player.transform.position) <= attackDistance) && (attackCdTimer >= attackCooldown))
        {
            agent.isStopped = true;
            state = State.Attacking;
            attackTimer = 0;
            attackCdTimer = 0;
            return;
        }
    }
}
