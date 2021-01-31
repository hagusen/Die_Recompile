using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelStats : UnitStats
{

    public BulletExplosion prefab;

    public override void Damage(int damage, DamageType type) {
        stats.health -= damage;


        if (stats.health <= 0) {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }


}
