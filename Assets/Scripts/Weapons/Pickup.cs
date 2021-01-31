using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {


    [SerializeField]
    PickupType type;

    public MeshRenderer mesh;
    private float lifetime;

    public void SetValues(float lifetime) {
        this.lifetime = lifetime;
        mesh.material.SetColor("_Color_Emission", Color.white);
        StartCoroutine(Blink());
    }

    IEnumerator Blink() {

        int timer = 1;


        yield return new WaitForSeconds(lifetime / 2);
        lifetime = lifetime / 2;

        while (0 >= lifetime) {
            mesh.material.SetColor("_Color_Emission", Color.white);
            yield return new WaitForSeconds(.05f);
            mesh.material.SetColor("_Color_Emission", Color.black);
            yield return new WaitForSeconds(1);
            timer++;
            lifetime -= 1.05f;
        }
        PickupSpawner.instance.ReturnPickup(this, type);
        //Destroy(gameObject);

    }


    public PickupType TakePickup() {

        var temp = EffectPool.ins.Get((int)EffectType.Item_Pickup);
        temp.SetValues(transform.position);
        temp.gameObject.SetActive(true);
        PickupSpawner.instance.ReturnPickup(this, type);
        return type;
    }


}
