using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float timeToLive;
    
    private float timer;
    private bool playerHit;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        playerHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == Manager.Instance.player) && (!playerHit))
        {
            Manager.Instance.playerStats.Damage(damage, DamageType.Explosive);
            playerHit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
