using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREATED BY "John Boman"
//USED FOR "bullet..."
public class Bullet : MonoBehaviour {

    public BulletInfo info;
    public BulletExplosion expPrefab;
    public GameObject psEffectSmoke;
    public LayerMask bounceLayer;
    public Vector3 standardSize;
    private Rigidbody rb;

    private float speedMultiplier = 1;
    private float damageMultiplier = 1;



    public void SetBulletInfo(BulletInfo _info) {
        info = _info;
    }

    public void SetAffixes(float speedMult, float damagemult) {
        speedMultiplier = speedMult;
        damageMultiplier = damagemult;
    }

    void OnEnable() {

        rb = GetComponent<Rigidbody>();
    }
    void OnDisable() {

        transform.localScale = standardSize;
    }

    void OnValidate() {

        standardSize = transform.localScale;
    }

    public void OnStart() {

        //x = 1;

        transform.localScale = new Vector3(standardSize.x * info.size.x, standardSize.y * info.size.y, standardSize.z * info.size.z);
        rb.velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = true;
        rb.drag = info.drag;
        rb.AddRelativeForce(0, 0, info.speed * speedMultiplier);
        BounceRaycast(50);

        Invoke("RemoveSelf", info.lifetime);
    }

    public void ArcBullet(Vector3 startPos, Vector3 targetPos, Vector3 halfwayPos, float t) {


        GetComponent<BoxCollider>().enabled = false;

        StartCoroutine(MoveBezier(startPos, halfwayPos, targetPos, t));

    }

    IEnumerator MoveBezier(Vector3 p0, Vector3 p1, Vector3 p2, float arcTime) {
        float t = 0;
        while (t <= arcTime) {
            transform.position = Bezier.EvaluateQuadratic(p0, p1, p2, t / arcTime);
            t += Time.deltaTime;
            yield return null;
        }

        RemoveSelf();
    }

    void FixedUpdate() {
        if (bounce <= 0) {
            BounceRaycast(rb.velocity.magnitude);
        }
        else {
            bounce--;
        }
    }
    int bounce = 0;
    float x = 1;
    private void BounceRaycast(float magnitude) {

        if (info.canBounce) {
            RaycastHit hit;
            Vector3 pos = new Vector3(transform.position.x, 1, transform.position.z);

            if (Physics.Raycast(pos, transform.forward, out hit, Time.fixedDeltaTime * magnitude, bounceLayer)) {

                WallEffect(hit.transform.position - transform.forward * 0.5f);
                Vector3 dir = Vector3.Reflect(transform.forward, hit.normal);
                var rot = 90 - Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
                //x = x * 1.1f;
                rb.velocity = transform.forward * rb.velocity.magnitude;
                bounce = 2;
            }
        }
    }

    private void WallEffect(Vector3 pos) {

        if (info.wallEffect) {

            var temp = EffectPool.ins.Get((int)EffectType.Ricochet);
            temp.SetValues(pos);

            temp.gameObject.SetActive(true);
        }

        //Sound effect?


    }

    private void OnCollisionEnter(Collision other) {// this is horrible
        //Debug.Log("COLLISION");
        //print(other.collider.name);





    }

    void OnTriggerEnter(Collider other) {

        
        
        if (other.GetComponent<TagsScript>()) {
            var tags = other.GetComponent<TagsScript>();
            if (!tags.HaveTag("Invulnerable")) {
                if (!tags.HaveTag("Wall") || info.canDamageWalls) { // <----------------------------------------------------------------------------------------------------------------------------
                    if (!tags.HaveTag(gameObject.tag) && !tags.HaveTag("Bullet"))
                    {

                        if (other.GetComponent<Enemy>()) {
                            other.GetComponent<Enemy>().GetKnockback(info.knockbackPower);
                        }
                        if (other.GetComponent<UnitStats>())
                        {
                            if (other.GetComponent<UnitStats>().stats.health > 0) {
                                other.GetComponent<UnitStats>().Damage((int)(info.damage * damageMultiplier), DamageType.Normal);

                                if (!info.canBounce && !info.canPierce) {
                                    RemoveSelf();
                                }
                            }
                        }
                    }
                    
                    /*if ((gameObject.tag == "Friendly" && !tags.HaveTag("Friendly")) || (gameObject.tag == "Hostile" && !tags.HaveTag("Hostile"))) {
                        if (other.GetComponent<UnitStats>()) {

                            other.GetComponent<UnitStats>().Damage(info.damage);
                            if (!info.canBounce) {
                                RemoveSelf();
                            }

                        }
                    }*/
                } else {
                    //other.GetComponent<WallStats>().Damage(info.damage);
                }

            }

            if (tags.HaveTag("Wall")) {
                WallEffect(other.transform.position - transform.forward * 0.5f);
                PlayImpact();
              
            }


            if (!(tags.HaveTag("Wall") && info.canBounce)) {

                if (!tags.HaveTag("Weapon") && !tags.HaveTag("Bullet") && !tags.HaveTag(gameObject.tag)) {
                    if (!info.canPierce) {
                        RemoveSelf();
                    }
                }
            }
        }


    }


    private void RemoveSelf() {

        CancelInvoke("RemoveSelf");
        if (info.isExplosive)
            Instantiate(expPrefab, transform.position, Quaternion.identity);

        rb.velocity = Vector3.zero;

        if (psEffectSmoke) {
            //psEffectSmoke.transform.SetParent(null);
            //Destroy(psEffectSmoke, 5);
            //psEffectSmoke.GetComponent<ParticleSystem>().Stop();
        }

        var temp = EffectPool.ins.Get((int)EffectType.Bullet_Despawn);
        temp.SetValues(transform.position);
        temp.gameObject.SetActive(true);

        BulletPool.ins.ReturnToPool(this, (int)info.type);
        //Destroy(gameObject);


    }
    [FMODUnity.EventRef]
    public string OnWallImpact;
    FMOD.Studio.EventInstance impactAudio;
    public void PlayImpact()
    {
        impactAudio = FMODUnity.RuntimeManager.CreateInstance(OnWallImpact);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(impactAudio, GetComponent<Transform>(), GetComponent<Rigidbody>());
        impactAudio.start();
    }


}
