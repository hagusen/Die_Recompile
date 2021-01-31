using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool<Effect>
{








}


public enum EffectType {
    Bloodexplosion,
    Bloodsplat,
    Dust,
    Explosion,
    Muzzleflash,
    Ricochet,
    Air_Particles,
    Dust_Destruction, // on wall explode
    Dust_walk,        // On footstep
    Item_Pickup,       // pickups
    Stones,
    Gib_Meat,
    Bullet_Casing,
    Shell_Casing,
    Bullet_Despawn,
    Dust_Dash
}