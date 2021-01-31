///CREATED BY "John Klingh Ramsin"
///USED FOR "Attacking as a collider"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    private Renderer rend;

    private void OnTriggerEnter(Collider other)
    {
        rend = GetComponent<Renderer>();

        if (other.gameObject == Manager.Instance.player)
        {
            rend.material.color = Color.red;
            Manager.Instance.playerStats.Damage(1, DamageType.Normal);
            Debug.Log("collision");
        }
    }
}
