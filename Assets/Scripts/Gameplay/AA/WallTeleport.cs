using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTeleport : MonoBehaviour {
    internal event Action<Vector3> OnTouchWall = (Vector3 teleportPosition) => { };

    [SerializeField]
    private Vector3 teleportPosition;

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            OnTouchWall(teleportPosition);
        }
    }
}
