using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {


    public Transform player;

    public float Damping = 4;

    public Vector3 dir;
    public float duration;
    public float randRotation = 2.5f;
    public float randPower = 0.05f;

    public static CameraShaker ins { get; private set; }
    public float explosionMaxDistance;
    public AnimationCurve explosionDistance;


    void Awake() {
        if (!ins) {
            ins = this;
        }
        else {
            Debug.LogWarning("More than one singleton of this type " + Faces.GetFace(faceType.Mad, 2));
        }
    }



    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            //StartCoroutine(Shake(.3f,.06f));
            ShakeOnceT(0.1f, dir, dir.magnitude);
            //StartCoroutine(ShakeOnce(duration, dir));

        }



        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * Damping);
    }


    public void ShakeOnceT(float dur, Vector3 direction, float power) {


        StartCoroutine(ShakeOnce(dur, direction, power));
        //Random.Range(-randomness, randomness)

    }
    public void ShakeOncetest(float dur, Vector3 direction, float power, Vector3 pos) {
        float dist = (player.position - pos).magnitude;

        if ((dist / explosionMaxDistance) > 1) {

        }
        else {
            float  mult = explosionDistance.Evaluate(Mathf.Abs((dist / explosionMaxDistance) - 1));
            StartCoroutine(ShakeOnceY(dur * (mult * 2), direction, power * mult));

        }

        //Random.Range(-randomness, randomness)

    }

    IEnumerator ShakeOnce(float duration, Vector3 direction, float power) {

        float elapsed = 0;
        var temp = /*Quaternion.AngleAxis(Random.Range(-randRotation, randRotation), Vector3.up) **/ direction;
        var temp2 = new Vector3(temp.x, temp.z, temp.y) * power /* (1 + Random.Range(-randPower, randPower))*/;
        do {
            transform.localPosition = Vector3.Lerp(transform.localPosition, temp2, elapsed / duration);

            elapsed += Time.deltaTime;

            yield return null;

        } while (elapsed < duration);

        //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(direction.x, direction.z), 1);


    }

    IEnumerator ShakeOnceY(float duration, Vector3 direction, float power) {

        float elapsed = 0;
        do {
            var temp = Quaternion.AngleAxis(Random.Range(-randRotation, randRotation), Vector3.up) * direction;
            var temp2 = new Vector3(temp.x, temp.z, temp.y) * power * (1 + Random.Range(-randPower, randPower));
            transform.localPosition = Vector3.Lerp(transform.localPosition, temp2, elapsed / duration);

            elapsed += Time.deltaTime;

            yield return null;

        } while (elapsed < duration);

        //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(direction.x, direction.z), 1);


    }

    IEnumerator Shake(float duration, float magnitude) {

        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;

        while (elapsed < duration) {

            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(x, 0, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
