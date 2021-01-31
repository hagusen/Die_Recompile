using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PickupUI : MonoBehaviour {

    public Text text;
    public Image[] images;


    public PickupType type;
    public float lifetime = 0.5f;

    public Vector3 startPos;
    public AnimationCurve speedCurve;

    public float speed;
    public string value;

    // speed upwards
    //acceleration
    // lifetime

    private RectTransform recTransform;
    private Camera mainCamera;
    private float timer;


    public void SetValues(Vector3 pos, string text, PickupType type) {
        startPos = pos;
        value = text;
        this.type = type;
    }

    void OnEnable() {

        mainCamera = Camera.main;
        recTransform = GetComponent<RectTransform>();
        timer = 0;

    }

    public void OnStart() {

        text.text = "+" + value;

        Invoke("FadeOut", lifetime / 1.2f);

    }


    void LateUpdate() { // change to late update if messy



        timer += Time.deltaTime;



        Vector2 worldPos = mainCamera.WorldToScreenPoint(startPos);
        transform.position = worldPos + Vector2.up * speedCurve.Evaluate(timer / lifetime) * speed;








        if (timer > lifetime) {
            PickupUIController.ins.ReturnToPool(this, (int)type);
        }




    }

    private void FadeOut() {
        text.CrossFadeAlpha(0, lifetime/6, false);
        foreach (var img in images) {
            img.CrossFadeAlpha(0, lifetime/6, false);
        }
    }
}
