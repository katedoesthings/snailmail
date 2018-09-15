using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSection : MonoBehaviour {
    public float raycastDistance;
    public float stickyStrength = 50;

    public float gravityStrength = 20;

    public Rigidbody rb { get; private set; }
    private Material mat;
    public bool grounded { get; private set; }

    public bool isStuck => sticky && grounded;
    public Vector3 gravityNormal { get; private set; }

    public bool sticky = true;

    private float drag;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        drag = rb.drag;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        grounded = false;
        gravityNormal = Vector3.zero;
        if (Physics.Raycast(new Ray(transform.position, -transform.up), out hit, raycastDistance + 0.45f, 1)) {
            if (sticky) {
                gravityNormal = hit.normal;
                rb.AddForceAtPosition(gravityNormal * -stickyStrength, transform.position - transform.up * 0.5f, ForceMode.Acceleration);
            }
            grounded = true;
        }

        //rb.drag = rb.angularDrag = isStuck ? drag : 0;
        if (!isStuck) {
            rb.AddForceAtPosition(Vector3.down * gravityStrength, transform.position - transform.up * 0.5f, ForceMode.Acceleration);
        }

        mat.color = grounded ? Color.white : Color.red;
	}
}
