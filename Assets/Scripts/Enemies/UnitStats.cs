///CREATED BY "John Klingh Ramsin"
///USED FOR "Holding the stats of the player and editing those stats"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public StatPage stats;
    public int maxHp;


    public struct StatPage
    {
        //Variable declaration
        public int health;

        //Constructor
        public StatPage(int health)
        {
            this.health = health;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        DoStart();
    }

    protected virtual void DoStart()
    {
        stats = new StatPage(maxHp);
    }

    public virtual void Damage(int damage, DamageType type)
    {
        stats.health -= damage;
        if(stats.health <= 0)
        {
            if (PickupSpawner.instance) {
                //PickupSpawner.instance.SpawnPickup(transform.position);
            }
            gameObject.SetActive(false);
        }
    }

}

public enum DamageType {
    Normal,
    Explosive
}
