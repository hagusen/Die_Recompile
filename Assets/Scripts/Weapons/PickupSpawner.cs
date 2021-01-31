using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : ObjectPool<Pickup> {

    public AmmoInfo info;
    AmmoType mainWeaponType;

    Pickup obj;

    public static PickupSpawner instance;


    void Awake() {
        if (!instance) {
            instance = this;
        }
        else {
            Debug.LogWarning("More than one singleton of this type " + Faces.GetFace(faceType.Mad, 2));
        }
        WeaponManager.OnWeaponTypeChanged += SetWeaponType;

        OnAwake();
    }


    public void SpawnAmmoPickup(Vector3 positon) {



        positon = new Vector3(positon.x, 2f, positon.z);

        if (Random.Range(0f, 1f) <= info.preferMainWeapon) {
            obj = Get((int)mainWeaponType);
        }
        else {
            obj = Get(Random.Range(0, (int)AmmoType.Explosive));
        }


        obj.transform.position = positon;
        obj.gameObject.SetActive(true);
        obj.SetValues(info.pickupLifetime);

    }

    public void SpawnResourcePickup(Vector3 position) {
        Debug.Log("Resource");
        position = new Vector3(position.x, 3f, position.z);

        obj = Get((int)PickupType.Resource);
        obj.transform.position = position;
        obj.gameObject.SetActive(true);
        obj.SetValues(info.pickupLifetime);
    }

    public void SpawnHealthPickup(Vector3 positon) {


        obj = Get((int)PickupType.Health);
        obj.transform.position = positon;
        obj.gameObject.SetActive(true);
        obj.SetValues(info.pickupLifetime);

    }


    public void SpawnPickup(Vector3 pos) {

        float temp = Random.Range(0f, 1f);

        if (temp <= info.ammoDropchance) {
            SpawnAmmoPickup(pos);
        }
        else if (temp <= info.ammoDropchance + info.healthDropChance) {
            SpawnHealthPickup(pos);
        }
        if (Random.Range(0.0f, 1.0f) <= info.resourceDropChance) {
            SpawnResourcePickup(pos);
        }

    }


    public void ReturnPickup(Pickup obj, PickupType type) {
        ReturnToPool(obj, (int)type);
    }

    public void SetWeaponType(AmmoType type) {
        mainWeaponType = type;
    }


}



public enum PickupType {
    Bullet,
    Shell,
    Explosive,
    Energy,

    Health,//temp
    Resource

}

