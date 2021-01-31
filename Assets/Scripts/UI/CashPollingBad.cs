using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPollingBad : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Manager.Instance.cashText.text = Manager.Instance.flowchart.GetIntegerVariable("Cash").ToString();
    }
}
