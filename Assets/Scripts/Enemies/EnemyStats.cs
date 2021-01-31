using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : UnitStats
{
    protected Enemy self;

    /*
    public SkinnedMeshRenderer[] renderers;
    public Color onHitColor = Color.red;
    public float onHitTime = .1f;
    public Color onDeathColor = Color.grey;
    public float onDeathAlpha = 0.5f;
    */

    //public float sleep = 0.05f;
    // Start is called before the first frame update

    public EnemyAudio eAud;
    public bool ragdollWithWeapon;


    protected override void DoStart()
    {
        base.DoStart();
        self = gameObject.GetComponent<Enemy>();
    }

    public override void Damage(int damage, DamageType type)
    {
        stats.health -= damage;


        if (eAud != null)
        eAud.PlayOnEnemyHurt();
        //StartCoroutine(stop());

        if (stats.health <= 0 && !self.isRagdoll)
        {
            if(ragdollWithWeapon)
            {
                SetKinematic(false);
            }

            if (EffectPool.ins) {
                var temp = EffectPool.ins.Get((int)EffectType.Bloodexplosion);
                temp.SetValues(transform.position);
                temp.gameObject.SetActive(true);
            }

            if (PickupSpawner.instance) {
                PickupSpawner.instance.SpawnPickup(transform.position);
            }

            self.Die(type);
            Debug.LogError("dead");
            eAud.PlayOnEnemyDeath();
        }
        else {

            self.TakeDamage();

            if (EffectPool.ins)
            {
                var temp = EffectPool.ins.Get((int)EffectType.Bloodsplat);
                temp.SetValues(transform.position);
                temp.gameObject.SetActive(true);
            }
        }
        
    }
    /*
    void SetDeathColor() {
      
        foreach (var rend in renderers) {
            rend.material.SetColor("_DiffuseColor", onDeathColor);
            rend.material.SetFloat("_Dither", onDeathAlpha);
        }
    }
    void HitColor() {

        CancelInvoke("ResetColor");
        
        foreach (var rend in renderers) {
            rend.material.SetColor("_ColorOverride", onHitColor);
        }
        Invoke("ResetColor", onHitTime);

    }
    void ResetColor() {


        foreach (var rend in renderers) {
            rend.material.SetColor("_ColorOverride", Color.black);
        }
    }

    IEnumerator stop() {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(sleep);
        Time.timeScale = 1;

    }
    */
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
