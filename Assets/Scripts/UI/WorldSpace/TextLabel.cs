using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class TextLabel : MonoBehaviour
{

    public GameObject target;

    public GameObject textobj;
    //public GameObject background;

    public Vector2 padding;

    private RectTransform textRect;
    private RectTransform backgroundRect;

    private RectTransform recTransform;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        recTransform = GetComponent<RectTransform>();
        textRect = textobj.GetComponent<RectTransform>();
        //backgroundRect = background.GetComponent<RectTransform>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var x = mainCamera.WorldToScreenPoint(target.transform.position);
        recTransform.position= x;

        //backgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textRect.sizeDelta.x + padding.x);
        //backgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textRect.sizeDelta.y + padding.y);







    }

    public void SetText(string text) {

        textobj.GetComponent<Text>().text = text;
    }

    public void SetTarget(GameObject other) {
        target = other;
    }



}
