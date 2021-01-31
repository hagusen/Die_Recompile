using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUIController : ObjectPool<PickupUI>
{
    public static System.Action<PickupType, string, Vector3> AddPickupUI = delegate { };



    void Start() {


        AddPickupUI += AddPickUp;
    }

    private void OnDestroy() {
        AddPickupUI -= AddPickUp;

    }

    void AddPickUp(PickupType type, string amount, Vector3 pos) {

        PickupUI temp = Get((int)type);
        temp.SetValues(pos, amount, type);
        temp.gameObject.SetActive(true);
        temp.OnStart();

    }



}
