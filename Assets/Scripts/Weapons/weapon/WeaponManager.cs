using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;



//CREATED BY "John Boman"
//USED FOR "Holding/switching/grabbing and shoothing Weapons for the player"s
[RequireComponent(typeof(AmmoManager))]
public class WeaponManager : MonoBehaviour, IUseWeapon {

    public static System.Action<AmmoType> OnWeaponTypeChanged = delegate { };

    public Weapon pistolPrefab;

    public Weapon mainWeapon;
    public Weapon secondaryWeapon;
    public GameObject mainWPos;
    public GameObject SecondaryWPos;
    public Color onEquipColor = Color.white;
    public float onEquipColorTime = 0.1f;
    public bool FreeFireForEditor;
    public WeaponHolderPoint weaponHoldPoint;


    //
    public float bulletDamageMod = 1.0f, shotgunDamageMod = 1.0f;
    public float bulletFirerate = 1.0f;
    public int shotgunBurst = 0;
    public float explosiveFirerate = 1f;
    public int explosiveMultishotMultiplier = 1;
    //
    [Range(0, 1)]
    public float lowAmmo = 0.2f;

    protected Rigidbody rb;
    protected Camera mainCamera;
    protected Animator animationController;
    protected AmmoManager ammoManager;

    protected PlayerAudio pAud;

    public void Reset() {
        //set wp to pistol 
        ammoManager.Reset();

        if (mainWeapon) {
            mainWeapon.transform.DetachChildren();
            mainWeapon.Deactivate();
            Destroy(mainWeapon.gameObject);

        }
        if (secondaryWeapon) {
            secondaryWeapon.transform.DetachChildren();
            secondaryWeapon.Deactivate();
            Destroy(secondaryWeapon.gameObject);
        }

        mainWeapon = null;
        secondaryWeapon = null;
        mainWeapon = Instantiate(pistolPrefab);


        if (mainWeapon) {
            mainWeapon.managerScript = this;

            if (gameObject.GetComponent<TagsScript>().HaveTag("Friendly")) {
                mainWeapon.tag = "Friendly";
            }
            OnWeaponTypeChanged(mainWeapon.bulletInfo.type);
        }
        mainWeapon.transform.SetParent(mainWPos.transform, false);
        mainWeapon.Activate();


    }

    void Awake() {

        TryGetComponent<PlayerAudio>(out pAud);

        if (mainWeapon) {
            mainWeapon.managerScript = this;

            if (gameObject.GetComponent<TagsScript>().HaveTag("Friendly")) {
                mainWeapon.tag = "Friendly";
            }
            OnWeaponTypeChanged(mainWeapon.bulletInfo.type);
        }

        ammoManager = GetComponent<AmmoManager>();
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        animationController = gameObject.GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update() {
        DoUpdate();
    }

    protected void SwitchWeapon() {


        var temp = mainWeapon;
        mainWeapon = secondaryWeapon;
        secondaryWeapon = temp;

        if (mainWeapon) {
            mainWeapon.transform.SetParent(mainWPos.transform, false);
            mainWeapon.Activate();
            OnWeaponTypeChanged(mainWeapon.bulletInfo.type);
        }

        if (secondaryWeapon) {
            secondaryWeapon.transform.SetParent(SecondaryWPos.transform, false);
            secondaryWeapon.Deactivate();
        }
    }

    protected void GrabWeapon() {
        if (pAud) {
            pAud.PlayGrabWeapon();
        }
        if (mainWeapon) {
            ResetColor();
        }
        if (!secondaryWeapon) {
            SwitchWeapon();
        }
        bool haveAmmo;
        bool isEmpty;
        weaponHoldPoint.TakeWeapon(mainWeapon, out mainWeapon, out isEmpty, out haveAmmo);

        if (!mainWeapon) { //if main weapon null
            SwitchWeapon();
        }

        mainWeapon.managerScript = this;
        mainWeapon.transform.SetParent(mainWPos.transform, false);
        mainWeapon.Activate();

        if (isEmpty) {
            weaponHoldPoint = null;
        }
        if (haveAmmo) {
            ammoManager.AddAmmoFromWeapon(mainWeapon.bulletInfo.type);
        }
        if (mainWeapon) {
            OnWeaponTypeChanged(mainWeapon.bulletInfo.type);
        }

        if (GetComponent<TagsScript>().HaveTag("Friendly")) {
            mainWeapon.tag = "Friendly";
        }
    }

    bool haveFired = false;

    protected virtual void DoUpdate() {
        if (Input.GetButtonDown("SwitchWeapon") && secondaryWeapon && !mainWeapon.IsFiring()) { // Q
            if (pAud) {
                pAud.PlaySwitchWeapon();
            }
            SwitchWeapon();
            OnEquipped();
        }

        if (Input.GetButtonDown("Grab") && weaponHoldPoint) {  // E
            if (mainWeapon) {
                if (!mainWeapon.IsFiring()) {
                    GrabWeapon();
                }
            }
            else {
                GrabWeapon();
            }


            OnEquipped();
        }

        if (mainWeapon) {

            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit)) {



                var dist = (transform.position - hit.point).magnitude;

                mainWeapon.Aim(dist);

                if (Input.GetButton("Fire1") && mainWeapon.CheckCooldown() && (Manager.Instance.playerStats.floor != -1 || FreeFireForEditor)) { // Try to fire the gun
                    if (CheckWall()) {
                        haveFired = true;
                        if (ammoManager.UseAmmo(mainWeapon.bulletInfo.type, mainWeapon.info.ammoLostPerShot)) {
                            if (mainWeapon.bulletInfo.type == AmmoType.Bullet) {
                                mainWeapon.damageMod = bulletDamageMod;
                                mainWeapon.firerateMod = bulletFirerate;
                            }
                            if (mainWeapon.bulletInfo.type == AmmoType.Shell) {
                                mainWeapon.damageMod = shotgunDamageMod;
                                mainWeapon.burstMod = shotgunBurst;
                            }

                            if (mainWeapon.bulletInfo.type == AmmoType.Explosive) {
                                mainWeapon.firerateMod = explosiveFirerate;
                                mainWeapon.multishotMod = explosiveMultishotMultiplier;
                            }
                            mainWeapon.Fire(dist);
                            //animationController.SetTrigger("shooting");

                        }
                        else {

                            pAud.PlayNoAmmoShot();

                        }

                    }
                    else {
                        haveFired = false;
                        //feedback??
                    }
                }

                if (Input.GetButtonUp("Fire1") && haveFired) {
                    haveFired = false;
                    mainWeapon.StopFiring();
                }

                if (ammoManager.CheckLowAmmo(mainWeapon.bulletInfo.type, lowAmmo)) {
                    pAud.PlayLowAmmo();
                }

            }
        }
    }

    private bool CheckWall() {
        //Debug.Log(mainWeapon.info.wallcheckDistance * 4);
        if (mainWeapon.info.wallcheckDistance > 0) {

            Vector3 pos = new Vector3(transform.position.x, 1, transform.position.z);
            if (Physics.Raycast(pos, transform.forward, mainWeapon.info.wallcheckDistance * 2, 1 << 10)) {
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }

    }

    public void TakeKnockback(float pwr) {
        rb.AddRelativeForce(0, 0, -pwr);
        try {
            CameraShaker.ins.ShakeOnceT(mainWeapon.info.CameraRecoilDuration, -transform.forward, mainWeapon.info.CameraRecoilForce);
        }
        catch (System.Exception) {

            throw;
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("WeaponHoldPoint")) {
            weaponHoldPoint = other.GetComponent<WeaponHolderPoint>();
        }
    }
    void OnTriggerStay(Collider other) {
        if (weaponHoldPoint == null) {

            if (other.CompareTag("WeaponHoldPoint")) {
                weaponHoldPoint = other.GetComponent<WeaponHolderPoint>();
            }
        }

    }

    void OnTriggerExit(Collider other) {

        if (other.CompareTag("WeaponHoldPoint")) {
            //weaponHoldPoint = null;
        }
    }

    void OnEquipped() {

        mainWeapon.GetComponent<MeshRenderer>().material.SetColor("_ColorOverride", onEquipColor);
        Invoke("ResetColor", onEquipColorTime);
    }

    void ResetColor() {
        mainWeapon.GetComponent<MeshRenderer>().material.SetColor("_ColorOverride", Color.black);

    }

    void OnDisable() {

        if (mainWeapon) {
            mainWeapon.Deactivate();
        }
        TextLabelController.ClearAllTextLabels(0);

    }
    void OnEnable() {

        if (mainWeapon) {
            mainWeapon.Activate();
        }
    }

    public void ShotgunBurstUpgrade() {
        shotgunBurst += 1;
    }
    public void ShotgunDamageUpgrade(float increment) {
        shotgunDamageMod += increment;
    }
    public void BulletFirerateUpgrade(float increment) {
        bulletFirerate *= increment;
    }
    public void BulletDamageUpgrade(float increment) {
        bulletDamageMod += increment;
    }
    public void ExplosiveFirerateUpgrade(float increment) {
        explosiveFirerate *= increment;
    }
    public void ExplosiveMultishotUpgrade() {
        explosiveMultishotMultiplier += 1;
    }
}
