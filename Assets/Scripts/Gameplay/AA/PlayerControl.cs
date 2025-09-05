using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private GameObject playerGo;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float speed = 6.0f;

    [SerializeField]
    private WallTeleport[] walls;

    private void Start() {
        for (int i = 0; i != walls.Length; ++i) {
            walls[i].OnTouchWall += OnTouchWall;
        }
    }
    private void OnDetroy() {
        for (int i = 0; i != walls.Length; ++i) {
            walls[i].OnTouchWall -= OnTouchWall;
        }
    }

    private void OnTouchWall(Vector3 teleportPosition) {
        characterController.transform.position = teleportPosition;
    }

    // Update is called once per frame
    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);

    }
}
