using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour {

    public int damage = 10;

    public Effect test;
    public float lifetime;

    [Header("ScreenShake")]
    public float duration = .2f;
    public float intensity = 1;

    // Start is called before the first frame update
    void OnEnable() {


        Effect temp;


        temp = EffectPool.ins.Get((int)EffectType.Explosion);


        temp.SetValues(gameObject.transform.position);
        temp.gameObject.SetActive(true);
        test = temp;

        if (CameraShaker.ins) {
            CameraShaker.ins.ShakeOncetest(duration, Vector3.up, intensity, transform.position);
        }

        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame-
    void Update() {




    }

    void OnTriggerEnter(Collider other) {

        if (other.GetComponent<TagsScript>()) {
            var tags = other.GetComponent<TagsScript>();
            if (!tags.HaveTag("Invulnerable")) {
                if (other.GetComponent<UnitStats>()) {
                    other.GetComponent<UnitStats>().Damage(damage, DamageType.Explosive);
                }

            }

        }
    }
}
