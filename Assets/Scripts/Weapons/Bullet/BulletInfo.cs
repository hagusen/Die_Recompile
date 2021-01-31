using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREATED BY "John Boman"
//USED FOR "Storing different bullets info"


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletInfo")]
public class BulletInfo : ScriptableObject {

    public AmmoType type;
    

    [Min(0)]
    public int damage;
    public float speed;
    [Min(0)]
    public float lifetime;
    public bool canDamageWalls = false;
    public bool wallEffect = true;
    public float knockbackPower = 50;

    [Header("Affixes")]
    public Vector3 size = Vector3.one;
    public bool quickSizeFix = false;


    [Header("Shotgun")]
    [Min(0)]
    public float drag;
    public bool canBounce = false;
    public bool canPierce = false;
   // public LayerMask 

    [Header("Explosive")]
    public bool isExplosive;


#if UNITY_EDITOR
    [Space(40, order = 0)]
    [Header("EDITOR ONLY", order = 1)]
    [Tooltip("Write Any notes here: (example: this is the bullet for the shotgun etc.)")]
    [TextArea(5,40)]
    public string notesForDesigners;
    
#endif


    //bouncing?
    //explosive?









}
