///CREATED BY "John Klingh Ramsin"
///USED FOR "Main class of enemies"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [Header("Enemy")]
    protected GameObject player;
    protected NavMeshAgent agent;
    public GameObject ragdoll;
    public float ragdollLifetime = 10;
    public float knockbackMultiplier = 1;

    [Header("Effects")]
    public Renderer[] renderers;

    public Color onHitColor = Color.white;
    public float onHitTime = .15f;
    public Color onDeathColor = Color.grey;

    private float onDeathAlpha = .8f;

    public bool isRagdoll = false;




    protected State state;

    public enum State {
        Moving,
        Attacking,
        Idle
    }

    void Start() {
        player = Manager.Instance.player;
        agent = GetComponent<NavMeshAgent>();
        state = State.Idle;
        renderers = GetComponentsInChildren<Renderer>();
        DoStart();
    }

    public virtual void DoStart() { }

    public virtual void Attack() { }

    public virtual void Move() { }

    public virtual void Idle() { }

    public virtual void Die(DamageType type) {
        ragdoll.SetActive(true);
        SetDeathColor();
        isRagdoll = true;

        Destroy(agent);
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Animator>());

        GameObject tmp = Manager.Instance.enemyBloodPool.CreatePool();
        tmp.transform.position = new Vector3(transform.position.x, Random.Range(0, .1f) + 1.05f, transform.position.z);


        if (GetComponent<EnemyAudio>()) {
            GetComponent<EnemyAudio>().PlayOnEnemyDeath();
        }

        if (EffectPool.ins && type == DamageType.Explosive) {
            var temp = EffectPool.ins.Get((int)EffectType.Gib_Meat);
            temp.SetValues(transform.position + Vector3.up);
            temp.gameObject.SetActive(true);
        }
        if (type == DamageType.Explosive) {
            Destroy(gameObject);
        }
        else {

            Destroy(gameObject, ragdollLifetime);

            var temp = EffectPool.ins.Get((int)EffectType.Gib_Meat);
            temp.SetValues(transform.position + Vector3.up);
            temp.gameObject.SetActive(true);

            //Destroy(this);
        }
    }
    IEnumerator FadeOut() {


        yield return new WaitForSeconds(0.75f * ragdollLifetime);
        float timer = 0;
        while (timer < ragdollLifetime) {
            timer += Time.deltaTime;
            foreach (var rend in renderers) {
                rend.material.SetFloat("_Dither", onDeathAlpha * Mathf.Abs((timer / (ragdollLifetime - 0.75f * ragdollLifetime)) - 1));
            }
            yield return null;
        }
    }



    public virtual void GetKnockback(float power) { // from bullets.
        //StartCoroutine(Knockback(power));

    }

    IEnumerator Knockback(float power) {

        agent.updatePosition = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().AddRelativeForce(0, 0, -power * knockbackMultiplier);

        yield return new WaitForSeconds(0.1f);
        agent.updatePosition = true;


    }

    public virtual void TakeDamage()
    {
        HitColor();
    }


    void SetDeathColor() {
        //ResetColor();
        CancelInvoke("ResetColor");
        foreach (var rend in renderers) {
            rend.material.SetColor("_DiffuseColor", onDeathColor);
            rend.material.SetFloat("_Dither", onDeathAlpha);
        }
        HitColor();
        StartCoroutine(FadeOut());
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

    public void Update() {
        if (!isRagdoll) {
            DoUpdate();
        }
    }

    public virtual void DoUpdate() {
        switch (state) {
            case State.Attacking:
                Attack();
                break;

            case State.Moving:
                Move();
                break;

            case State.Idle:
                Idle();
                break;
        }
    }

    public void SetKinematic(bool newValue) {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies) {
            rb.isKinematic = newValue;
        }
    }
}
