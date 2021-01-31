using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStats : UnitStats
{
    public UIHealth uihealth;
    public GameObject respawnPoint;
    public float timeDead;
    
    public int floor = -1;
    public Flowchart flow;
    public PlayerAudio pAud;
    public GameObject evilPlayer;

    [Header("OnHitShake")]
    public float duration = 0.4f;
    public float power = 1.5f;
    [Header("OnHitBlood")]
    public PostProcessVolume screenEffect;


    protected override void DoStart()
    {
        base.DoStart();
        SetKinematic(true);
        GetComponent<Animator>().enabled = true;
        GetComponent<PlayerMovementScript>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<WeaponManager>().enabled = true;
        previousWeapon = GetComponent<WeaponManager>().pistolPrefab;
        floor = -1;

        GetComponent<WeaponManager>().Reset();

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.transform.position;
        }

        if (uihealth != null)
        {
            uihealth.SetMax(maxHp);
            uihealth.Ping(stats.health);
        }
        else
        {
            Debug.LogWarning("No UI element set");
        }
    }

    public override void Damage(int damage, DamageType type)
    {
        stats.health -= damage;
        pAud.PlayOnPlayerHurt();
        StartCoroutine(BloodEffect());
        CameraShaker.ins.ShakeOncetest(duration, Vector3.down, power, transform.position);


        if (stats.health <= 0)
        {
            stats.health = 0;
            pAud.PlayOnPlayerDeath();
            SetKinematic(false);
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerMovementScript>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<WeaponManager>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine("Respawn");
        }


        if (uihealth != null)
        {
            uihealth.Ping(stats.health);
        }
    }


    IEnumerator BloodEffect() {


        screenEffect.weight = 1;
        yield return new WaitForSeconds(0.2f);


        while(stats.health <= 0) {
            yield return null;
        }


        while(screenEffect.weight > 0) {

            screenEffect.weight -= Time.deltaTime * 2;
            yield return null;
        }
        screenEffect.weight = 0;

        yield return null;
    }

    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Resource")) {
            other.GetComponent<Pickup>().TakePickup();
            int tmp = flow.GetIntegerVariable("Cash");
            flow.SetIntegerVariable("Cash", tmp + Random.Range(5, 40 + 1));

            Manager.Instance.cashText.text = flow.GetIntegerVariable("Cash").ToString();

        }

        if (other.CompareTag("Health")) {

            other.GetComponent<Pickup>().TakePickup();
            stats.health += 1;
            PickupUIController.AddPickupUI(PickupType.Health, "1", transform.position);
            pAud.PlayHealthPickup();
            if (stats.health > maxHp) {
                stats.health = maxHp;
            }

            uihealth.Ping(stats.health);
        }
    }

    public Weapon previousWeapon;
    IEnumerator Respawn()
    {
        GameObject eP;
        Manager.Instance.enemySpawner.floors[floor].IdleEnemies();

        flow.SetStringVariable("DiedOnFloor", GameObject.FindGameObjectWithTag("WorldGen").GetComponent<WorldGeneration>().GetFloorType(floor));

        previousWeapon = GetComponent<WeaponManager>().mainWeapon;
        GetComponent<WeaponManager>().mainWeapon = null;

        yield return new WaitForSeconds(timeDead);


        if (flow != null)
            flow.ExecuteBlock("Die");

        if (floor >= 0)
        {
            Manager.Instance.enemySpawner.RemoveEnemies(floor);
        }


        Vector3 tmp = gameObject.transform.position;
        tmp.y = 0;

        if (Manager.Instance.enemySpawner.floors[floor].bounds.Contains(tmp))
        {
            yield return eP = Instantiate(evilPlayer);
            eP.transform.position = gameObject.transform.position;
            Manager.Instance.enemySpawner.floors[floor].enemyInstances.Add(eP);
        }

        Destroy(previousWeapon.gameObject);
        yield return null;

        DoStart();
        pAud.PlayOnPlayerSpawn();

        //yield return null;

        yield return true;
    }

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
    public void UpgradeHP(int addedHp) {
        maxHp += addedHp;
        stats.health = maxHp;
        if (uihealth) {
            uihealth.SetMax(maxHp);
            uihealth.Ping(stats.health);
        }

    }
}
