using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStepper : MonoBehaviour{

    private void Start() {
        InvokeRepeating("RotateCoin", 0f, .2f);
    }

    private void RotateCoin() {
        transform.Rotate(new Vector3(0, 15f, 0));
    }
}
