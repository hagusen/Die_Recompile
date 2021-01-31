using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberCluster : MeleeEnemy
{
    public GameObject explosionPrefab;

    public override void Die(DamageType type)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        base.Die(type);
    }
}
