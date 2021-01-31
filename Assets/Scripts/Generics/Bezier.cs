using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier {




    public static Vector3 EvaluateQuadratic(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector3 p0 = Vector3.Lerp(a, b, t);
        Vector3 p1 = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(p0, p1, t);
    }

}
