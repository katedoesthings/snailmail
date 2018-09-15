using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour {
    private Rigidbody rb;

    public float velocity;
    public float acceleration;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = Vector3.MoveTowards(rb.velocity, v * velocity, acceleration * Time.deltaTime);
	}
}
