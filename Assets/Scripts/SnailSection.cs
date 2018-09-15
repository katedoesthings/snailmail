using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSection : MonoBehaviour {
    public float raycastDistance;
    public float stickyStrength = 50;

    public float gravityStrength = 20;
    public bool useGravity = true;
    public float radius;

    public Rigidbody rb { get; private set; }
    private Material mat;
    public bool grounded { get; private set; }

    public bool isStuck => sticky && grounded;
    public Vector3 gravityNormal { get; private set; }

    public SnailSection head;

    public bool sticky = true;


    public Transform meshNode;

    public Vector3 rotationToApply;
    private Quaternion quatToApply;

    private float drag;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        drag = rb.drag;
        quatToApply = Quaternion.Euler(rotationToApply);
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        grounded = false;
        gravityNormal = Vector3.zero;
        if (Physics.Raycast(new Ray(transform.position, -transform.up), out hit, raycastDistance + radius - 0.05f, 1)) {
            bool sticc = sticky && (head == this || !head.isStuck || head.gravityNormal == hit.normal);
            if (sticc) {
                gravityNormal = hit.normal;
                rb.AddForceAtPosition(gravityNormal * -stickyStrength, transform.position - transform.up * radius, ForceMode.Acceleration);
            }
            grounded = true;
        }

        //rb.drag = rb.angularDrag = isStuck ? drag : 0;
        if (!isStuck && useGravity) {
            rb.AddForceAtPosition(Vector3.down * gravityStrength, transform.position - transform.up * radius, ForceMode.Acceleration);
        }

        mat.color = grounded ? Color.white : Color.red;
	}

    private void LateUpdate() {
        if (meshNode) {
            meshNode.transform.position = transform.position;
            meshNode.transform.rotation = transform.rotation * quatToApply;
        }
    }
}
