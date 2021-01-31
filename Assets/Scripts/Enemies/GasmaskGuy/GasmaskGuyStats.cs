using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasmaskGuyStats : UnitStats
{
    protected Enemy self;

    public GasmaskAudio gAud;

    protected override void DoStart()
    {
        base.DoStart();

        self = gameObject.GetComponent<Enemy>();

        SetKinematic(true);
        GetComponent<Animator>().enabled = true;
        //GetComponent<NavMeshAgent>().enabled = true;
        //GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }
    public override void Damage(int damage, DamageType type)
    {
        stats.health -= damage;
        gAud.PlayGasmaskHurt();
        if (stats.health <= 0)
        {
            SetKinematic(false);
            GetComponent<Animator>().enabled = false;
            //GetComponent<Rigidbody>().isKinematic = false;

            if (EffectPool.ins)
            {
                var temp = EffectPool.ins.Get((int)EffectType.Bloodexplosion);
                temp.SetValues(transform.position);
                temp.gameObject.SetActive(true);
            }

            if (PickupSpawner.instance)
            {
                PickupSpawner.instance.SpawnPickup(transform.position);
            }


            self.Die(type);
            gAud.PlayGasmaskDeath();
        }

        else
        {
            self.TakeDamage();
            if (EffectPool.ins)
            {
                var temp = EffectPool.ins.Get((int)EffectType.Bloodsplat);
                temp.SetValues(transform.position);
                temp.gameObject.SetActive(true);
            }
        }
    }

    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
        foreach (Collider c in colliders)
        {
            c.enabled = !newValue;
        }
    }
}
