using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextLabelController : MonoBehaviour
{
    public static Action<GameObject, string> OnTextLabelAdded = delegate { };
    public static Action<GameObject> OnTextLabelRemoved = delegate { };
    public static Action<int> ClearAllTextLabels = delegate { };

    Dictionary<GameObject, TextLabel> textLabels = new Dictionary<GameObject, TextLabel>();


    public TextLabel textlabelPrefab;


    void Awake() {

        OnTextLabelAdded += AddTextLabel;
        OnTextLabelRemoved += RemoveTextLabel;
        ClearAllTextLabels += ClearTextLabels;

    }

    private void OnDestroy() {
        OnTextLabelAdded -= AddTextLabel;
        OnTextLabelRemoved -= RemoveTextLabel;
    }

    void AddTextLabel(GameObject target, string txt) {

        if (!textLabels.ContainsKey(target)) {

            var txtLabel = Instantiate(textlabelPrefab, transform);
            textLabels.Add(target, txtLabel);
            txtLabel.SetTarget(target);
            txtLabel.SetText(txt);
        }
    }


    void RemoveTextLabel(GameObject target) {

        if (textLabels.ContainsKey(target)) {

            Destroy(textLabels[target].gameObject);
            textLabels.Remove(target);
        }
    }

    void ClearTextLabels(int x) {

        foreach (var t in textLabels.Values) {
            Destroy(t.gameObject);
        }
        textLabels.Clear();
    }




}
