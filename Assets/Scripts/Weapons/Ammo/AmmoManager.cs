using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

    public AmmoInfo info;
    public AmmoBar ammoBar;

    public static System.Action<int> OnCurrentAmmoChanged = delegate { };

    //when bullet updates  ort curammo call a function

    public Ammo[] ammos;

    public float[] ammoUpgradeMultiplier;

    public bool UseAmmo(AmmoType type, int amount) {

        int i = (int)type;

        if (ammos[i].curAmmo > 0) { // if you have any ammo you can shoot!

            ammos[i].curAmmo -= amount;
            if (ammos[i].curAmmo <= 0)
                ammos[i].curAmmo = 0;


            AmmoChanged(ammos[i].curAmmo);
            if (ammoBar)
                ammoBar.SetAmmo(type, ammos[i].curAmmo);

            return true;

        } else {
            AmmoChanged(ammos[i].curAmmo);
            if (ammoBar)
                ammoBar.SetAmmo(type, ammos[i].curAmmo);

            return false;
        }


    }

    void AddAmmo(AmmoType type, float percentage) {

        int i = (int)type;
        ammos[i].curAmmo += (int)(ammos[i].maxAmmo * percentage);
        if (ammos[i].curAmmo > ammos[i].maxAmmo) {
            ammos[i].curAmmo = ammos[i].maxAmmo;
        }

        AmmoChanged(ammos[i].curAmmo);

        if (ammoBar)
            ammoBar.SetAmmo(type, ammos[i].curAmmo);
    }

    public bool CheckLowAmmo(AmmoType type, float percentage) {

        if (ammos[(int)type].curAmmo <= ammos[(int)type].maxAmmo * percentage) {
            return true;
        }
        else {
            return false;
        }
    }
    public void AddAmmoFromWeapon(AmmoType type) {

        AddAmmo(type, info.AmmoFromWeapon);

    }

    public void AddAmmoFromPickup(AmmoType type) {

        PickupUIController.AddPickupUI((PickupType)type, ((int)(CalcAmmoFromPickup(type) * ammos[(int)type].maxAmmo)).ToString(), transform.position);
        AddAmmo(type, CalcAmmoFromPickup(type));

    }


    float CalcAmmoFromPickup(AmmoType type) {

        float cur = (float)ammos[(int)type].curAmmo / ammos[(int)type].maxAmmo;
        float total = info.AmmoFromPickup;

        foreach (var item in info.extraAmmo) {

            if (item.under >= cur) {
                total += item.add;
            }
        }
        return total;
    }


    public void AmmoChanged(int ammo) {

        OnCurrentAmmoChanged(ammo);
    }

    public void WeaponTypeChanged(AmmoType type) {

        OnCurrentAmmoChanged(ammos[(int)type].curAmmo);
        if (ammoBar) {
            ammoBar.SetActive(type);
        }
    }


    void Awake() {
        ammos = new Ammo[info.ammos.Length];
        ammoUpgradeMultiplier = new float[info.ammos.Length];
        for (int i = 0; i < info.ammos.Length; i++) {
            ammos[i] = info.ammos[i];
            ammoUpgradeMultiplier[i] = 1.0f;
        }
        
        WeaponManager.OnWeaponTypeChanged += WeaponTypeChanged;
        Reset();
    }

    public void Reset() {

        initialize();
        if (ammoBar) {

            ammoBar.SetMaxAmmo(AmmoType.Bullet, ammos[0].maxAmmo);
            ammoBar.SetMaxAmmo(AmmoType.Shell, ammos[1].maxAmmo);
            ammoBar.SetMaxAmmo(AmmoType.Explosive, ammos[2].maxAmmo);
            ammoBar.SetMaxAmmo(AmmoType.Energy, ammos[3].maxAmmo);
        }

    }

    public void UpgradeMaxAmmo(int index, float multiplier) {
        ammoBar.SetMaxAmmo((AmmoType)index, (int)((float)ammos[index].maxAmmo * multiplier));
        ammoBar.SetAmmo((AmmoType)index, (int)((float)ammos[index].maxAmmo * multiplier));
        ammoUpgradeMultiplier[index] *= multiplier;
        initialize();
    }


    private void initialize() {
        

        for (int i = 0; i < info.ammos.Length; i++) { // constant
           // Debug.LogError(ammoUpgradeMultiplier[i]);
            ammos[i].maxAmmo = (int)((float)ammos[i].maxAmmo * ammoUpgradeMultiplier[i]);
            ammos[i].curAmmo = ammos[i].maxAmmo;
            ammoUpgradeMultiplier[i] = 1;
        }


    }


    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Ammo")) {

            AmmoType type = (AmmoType)other.GetComponent<Pickup>().TakePickup();
            AddAmmoFromPickup(type);

        }
    }

    void OnTriggerExit(Collider other) {


    }





}
