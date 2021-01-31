using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{

    [Min(0)]
    public float lifetime;
    public EffectType type;
    public bool haveParent = false;


    public void SetValues(Transform parent) {
        haveParent = true;
        if (parent) {

            transform.SetParent(parent, false);
        }
    }
    public void SetValues(Vector3 pos) {

        transform.position = pos;
    }

    void OnEnable()
    {

        if (!(lifetime == 0)) {
            Invoke("Remove", lifetime);
        }
        GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().Play();
        
    }
    void Update() {
   


    }

    void Remove() {
        
        EffectPool.ins.ReturnToPool(this, (int)type);

    }

}
