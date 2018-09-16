using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public Transform track;
    public float distance = 10;

    private Vector3 angles;

	// Use this for initialization
	void Start () {
        angles = transform.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        track = FindObjectOfType<SnailMovement>().transform;
	}

    private void Update() {
        angles.x -= Input.GetAxis("Mouse Y");
        angles.y += Input.GetAxis("Mouse X");
    }

    private void LateUpdate() {
        transform.eulerAngles = angles;


        transform.position = track.transform.position - transform.forward * distance;
    }
}
