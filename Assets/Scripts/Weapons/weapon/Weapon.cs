//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//CREATED BY "John Boman"
//USED FOR "A Weapon that can be used"

public enum State {
    Active,
    Deactived
}

//[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour {


    public State mode; // behövs den?
    public WeaponInfo info;
    [HideInInspector]
    public BulletInfo bulletInfo;
    public GameObject bulletFirePositon;
    private Vector3 bulletPos;

    [HideInInspector]
    public IUseWeapon managerScript;
    public bool IsFiring() { return firing; }



    private float firetimer = 0;
    private int counter;
    private bool firing = false;


    private float distanceToTarget;

    [SerializeField]
    public float damageMod = 1.0f, firerateMod = 1.0f;
    [SerializeField]
    public int burstMod = 0, multishotMod = 1;



    private delegate void RotationOutput(Quaternion rot, int i);

    private RotationOutput _typedelegate;




    //line renderers test
    public List<LineRenderer> lineRends = new List<LineRenderer>();
    public LineRenderer lrendPrefab;
    public int arcResolution = 15;
    //

    //Hitscan
    List<Vector3> hitscanPositions = new List<Vector3>();
    //


    WeaponAudio aud;

    void Awake() {
        bulletInfo = info.bulletInfo;
    }

    void Start() {


        TryGetComponent(out aud);

    }

    public void Deactivate() {
        mode = State.Deactived;
        //turn off aim stuff

        EnableLineRenderers(false);

    }

    public void Activate() {
        mode = State.Active;
        //turn on aim stuff or not?

        EnableLineRenderers(true);

    }

    void Update() {

        if (firetimer < info.reloadTime)
            firetimer += Time.deltaTime;

    }

    private float speedaffix = 1;

    public void setAffixes(float speedmult) {// struct with affixes?

        speedaffix = speedmult;

    }

    //User calls this  speciallyt player
    public void StopFiring() {
        if (aud) {
            aud.PlayOnStopFire();
        }
    }
    //User calls this
    public bool Fire(float dist) {

        distanceToTarget = dist;

        if (CheckCooldown()) { //check if active?

            firing = true;
            counter = info.fireTimes + burstMod;

            switch (info.weaponType) {
                case WeaponType.Standard:
                    _typedelegate = StandardBullet;
                    break;
                case WeaponType.Arc:
                    _typedelegate = ArcBullet;
                    break;
                case WeaponType.Hitscan:
                    _typedelegate = HitscanBullet;
                    break;
                case WeaponType.Melee:
                    _typedelegate = MeleeBullet;
                    break;
                default:
                    Debug.LogError("Weapon with no weaponType..?? UWU ");
                    break;
            }
            //if (CheckWall()) 
            InvokeRepeating("FireShoot", 0, info.timeBetweenFires);


            return true;
        }
        return false;

    }

    //User calls this
    public void Aim(float dist) {

        if (CheckCooldown()) { //check if active?????

            distanceToTarget = dist;


            //diffrent ways to aim diffrent weapons 
            //switch?
            switch (info.weaponType) {
                case WeaponType.Standard:
                    //nothing?
                    break;

                case WeaponType.Arc:
                    //display and calculate arc
                    // if line renderers not active -> activate // activate amount needed
                    RenderArc();
                    break;

                case WeaponType.Hitscan:
                    // display line??
                    RenderHitScan();
                    break;

                case WeaponType.Melee:
                    //nothing 
                    break;

                default:
                    break;
            }
        }

    }

    public bool CheckCooldown() {
        return (info.reloadTime * firerateMod) <= firetimer && !firing;
    }


    void FireShoot() {//dele
        if (aud) {
            aud.PlayAudio();
        }

        try {
            var temp = EffectPool.ins.Get((int)EffectType.Muzzleflash);
            temp.SetValues(bulletFirePositon.transform);
            temp.gameObject.SetActive(true);

            if (bulletInfo.type == AmmoType.Bullet) {
                temp = EffectPool.ins.Get((int)EffectType.Bullet_Casing);
                temp.SetValues(bulletFirePositon.transform.position);
                temp.gameObject.SetActive(true);
                temp.gameObject.transform.rotation = transform.rotation;
            }
            else if (bulletInfo.type == AmmoType.Shell) {
                temp = EffectPool.ins.Get((int)EffectType.Shell_Casing); 
                temp.SetValues(bulletFirePositon.transform.position);
                temp.gameObject.SetActive(true);
                temp.gameObject.transform.rotation = transform.rotation;
            }
            
        }
        catch (System.Exception) {

            throw;
        }
        if (bulletInfo.quickSizeFix) {
            bulletPos = new Vector3(bulletFirePositon.transform.position.x, 1.5f + (bulletInfo.size.y / 4), bulletFirePositon.transform.position.z);
        }
        else {
            bulletPos = new Vector3(bulletFirePositon.transform.position.x, 1.5f, bulletFirePositon.transform.position.z);

        }

        CalculateFiringAngle(_typedelegate); //dele

        //knockback on User
        managerScript.TakeKnockback(info.knockbackForce);


        // -ammo

        //For burst fire
        if (--counter == 0) {
            CancelInvoke("FireShoot");
            firetimer = 0;
            firing = false;
            //
        }


    }

    private void CalculateFiringAngle(RotationOutput function) {

        //Get angle
        var dir = -transform.forward;
        var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        float angleOffset = 0;
        if (info.firingAmount * (bulletInfo.type == AmmoType.Explosive ? multishotMod : 1) > 1)
            angleOffset = (info.firingAngle + 10 * (bulletInfo.type == AmmoType.Explosive ? multishotMod : 1)) / ((info.firingAmount * (bulletInfo.type == AmmoType.Explosive ? multishotMod : 1)) - 1); //can precalc

        var spread = Random.Range(0f, info.bulletSpread) - info.bulletSpread / 2;

        //Multishot
        for (int i = 0; i < info.firingAmount * (bulletInfo.type == AmmoType.Explosive ? multishotMod : 1); i++) {

            var rot = Quaternion.AngleAxis((((-angle - 90) - ((info.firingAngle + 10 * (bulletInfo.type == AmmoType.Explosive ? multishotMod : 0)) / 2)) + angleOffset * i) + spread, Vector3.up);

            function(rot, i);
        }
    }

    private Bullet Spawnbullet(Quaternion rot) {
         
        Bullet b = BulletPool.ins.Get((int)bulletInfo.type);

        if (!bulletInfo) {
            Debug.LogError("No bullet info chosen " + Faces.GetFace(faceType.Weird, 2));
        }
        b.SetBulletInfo(bulletInfo);
        b.SetAffixes(speedaffix, damageMod);
        b.tag = gameObject.tag;


        b.transform.SetPositionAndRotation(bulletPos, rot);
        b.gameObject.SetActive(true);
        b.OnStart();



        //Debug.DrawRay(transform.position, rot * Vector3.forward* 100,Color.red,3);

        return b;

        /*
        Bullet bullet = Instantiate(bulletPrefab, bulletFirePositon.transform.position, rot);// under global bullet folder // add pool
        bullet.tag = gameObject.tag;
        bullet.speedMultiplier = speedaffix;
        return bullet;
        */
    }

    private bool CheckWall() {

        if (Physics.Raycast(transform.position, transform.forward, info.wallcheckDistance * 2, 1 << 10)) {
            return false;
        }
        else {
            return true;
        }

    }

    private void StandardBullet(Quaternion rot, int i) {

        //if (CheckWall()) 
        Spawnbullet(rot);



    }

    private void ArcBullet(Quaternion rot, int i) {

        var obj = Spawnbullet(rot);

        Vector3 calcPos = transform.position + rot * Vector3.forward * distanceToTarget; // based on quaterinoinionoen
        var halfwayPos = ((calcPos - transform.position) / 2 + transform.position) + Vector3.up * info.arcHeight;

        obj.GetComponent<Bullet>().ArcBullet(transform.position, calcPos, halfwayPos, info.arcTime);


        EnableLineRenderers(false);

    }

    private void HitscanBullet(Quaternion rot, int i) {
        Spawnbullet(rot);

    }




    private void MeleeBullet(Quaternion rot, int i) {
        Spawnbullet(rot);

    }



    private void RenderArc() {

        if (lineRends.Count > 0 && !lineRends[0].enabled)
            EnableLineRenderers(true);

        CalculateFiringAngle(CalculateArcLines);



    }

    private void CalculateArcLines(Quaternion rot, int iterator) {

        Vector3 calcPos = transform.position + rot * Vector3.forward * distanceToTarget; // based on quaterinoinionoen
        var halfwayPos = ((calcPos - transform.position) / 2 + transform.position) + Vector3.up * info.arcHeight;

        if (lineRends.Count - 1 < iterator) {
            lineRends.Add(Instantiate(lrendPrefab, transform));
            lineRends[iterator].positionCount = arcResolution + 1;
        }

        for (int i = 0; i < arcResolution + 1; i++) {

            float t = (float)i / (float)arcResolution;
            lineRends[iterator].SetPosition(i, Bezier.EvaluateQuadratic(transform.position, halfwayPos, calcPos, t));

        }
    }



    private void RenderHitScan() {

        RaycastHit hit;
        Vector3 dir = transform.forward;
        dir = new Vector3(dir.x, 0.0f, dir.z);
        float maxDist = 1000;

        hitscanPositions.Clear();
        hitscanPositions.Add(transform.position);

        //print(dir);
        Reflect(transform.position, dir, maxDist);


        //Vector3 calcPos = transform.position + rot * Vector3.forward * (targetPos - transform.position).magnitude; // based on quaterinoinionoen



        if (lineRends.Count - 1 < 0) {
            lineRends.Add(Instantiate(lrendPrefab, transform));
            lineRends[0].positionCount = arcResolution;
        }


        lineRends[0].positionCount = hitscanPositions.Count;
        lineRends[0].SetPositions(hitscanPositions.ToArray());

    }

    private void Reflect(Vector3 position, Vector3 direction, float remainingDistance) {




        Vector3 refDir;
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, remainingDistance)) {
            refDir = Vector3.Reflect(direction, new Vector3(hit.normal.x, 0, hit.normal.z));
            hitscanPositions.Add(hit.point);
        }
        else {

            hitscanPositions.Add(position + direction * remainingDistance);
            return;
        }

        var distRemain = remainingDistance - (hit.point - position).magnitude;
        print(position - hit.point);
        if (distRemain <= 0) {
            return;
        }

        Reflect(hit.point, refDir, remainingDistance - distRemain);
    }



    private void EnableLineRenderers(bool x) {
        foreach (var lRend in lineRends) {
            lRend.enabled = x;
        }
    }

    public void OverrideBullet(BulletInfo info) {
        bulletInfo = info;
    }

}

