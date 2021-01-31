using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStats : UnitStats {



    public override void Damage(int damage, DamageType type) {
        stats.health -= damage;


        if (stats.health <= 0) {
            try {
                var temp = EffectPool.ins.Get((int)EffectType.Dust_Destruction);
                temp.SetValues(transform.position);
                temp.gameObject.SetActive(true);

                temp = EffectPool.ins.Get((int)EffectType.Stones);
                temp.SetValues(transform.position);
                temp.gameObject.SetActive(true);

                //PickupSpawner.instance.SpawnPickup(transform.position);     Spawn pickup
            }
            catch (System.Exception) {

                throw;
            }
            if (GetComponent<WallChanger>()) {

                this.GetComponent<WallChanger>().destroyWall();
            }
            else {
                Destroy(gameObject);
            }
        }

    }



}
