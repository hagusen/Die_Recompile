using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WallChanger : MonoBehaviour {
    [SerializeField]
    private MeshFilter brokenFloorMesh, brokenWallMesh;
    [SerializeField]
    private Material brokenFloorMaterial, brokenWallMaterial;

    public void destroyWall() {
        RaycastHit hit = new RaycastHit();
        LayerMask layer = LayerMask.GetMask("WallTiles");
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1, layer, QueryTriggerInteraction.Ignore)) {
            if (hit.collider.GetComponent<WallChanger>()) {
                hit.collider.GetComponent<WallChanger>().breakWall();

            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layer, QueryTriggerInteraction.Ignore)) {
            if (hit.collider.GetComponent<WallChanger>()) {
                hit.collider.GetComponent<WallChanger>().breakWall();

            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1, layer, QueryTriggerInteraction.Ignore)) {
            if (hit.collider.GetComponent<WallChanger>()) {
                hit.collider.GetComponent<WallChanger>().breakWall();

            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1, layer, QueryTriggerInteraction.Ignore)) {
            if (hit.collider.GetComponent<WallChanger>()) {
                hit.collider.GetComponent<WallChanger>().breakWall();

            }

        }
        this.gameObject.transform.SetParent(null);
        this.GetComponent<MeshRenderer>().material = brokenFloorMaterial;
        this.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, 0.60f, this.transform.position.z), Quaternion.Euler(new Vector3(Random.Range(0, 4) * 90, Random.Range(0, 4) * 90, Random.Range(0, 4) * 90)));
        this.GetComponent<MeshFilter>().mesh = brokenFloorMesh.sharedMesh;
        this.gameObject.GetComponent<TagsScript>().AddTag("Invulnerable");
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<WallStats>().enabled = false;
        this.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        this.gameObject.layer = 12;
        PlayExplosion();
    }

    public void breakWall() {
        this.gameObject.transform.SetParent(null);
        this.GetComponent<MeshRenderer>().material = brokenWallMaterial;
        this.transform.SetPositionAndRotation(this.transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 4) * 90, 0)));
        this.GetComponent<MeshFilter>().mesh = brokenWallMesh.sharedMesh;
    }

    FMOD.Studio.EventInstance wallDestroy;
    public void PlayExplosion() {
        wallDestroy = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Interacibles/Wall_destruction");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(wallDestroy, GetComponent<Transform>(), GetComponent<Rigidbody>());
        wallDestroy.start(); 

    }




}
