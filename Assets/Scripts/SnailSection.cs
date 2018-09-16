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
    public Vector3 groundNormal { get; private set; }

    public bool isStuck => sticky && grounded;
    public Vector3 stuckNormal { get; private set; }

    public SnailSection head;

    public bool sticky = true;


    public Transform meshNode;

    public Vector3 rotationToApply;
    public Quaternion quatToApply { get; private set; }
    public Vector3 potentialGround { get; private set; }

    private float drag;

    public bool onIce;

    public LayerMask groundLayers;

	// Use this for initialization
	void Awake () {
        onIce = false;
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        drag = rb.drag;
        quatToApply = Quaternion.Euler(rotationToApply);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default") && !isStuck && sticky) {
            potentialGround = collision.contacts[0].normal;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default")) {
            potentialGround = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        grounded = false;
        stuckNormal = Vector3.zero;
        groundNormal = Vector3.zero;
        if (Physics.Raycast(new Ray(transform.position, -transform.up), out hit, raycastDistance + radius - 0.05f, groundLayers)) {
            if(hit.collider.gameObject.layer == 0)
            {
                groundNormal = hit.normal;
                bool sticc = sticky && (head == this || !head.isStuck || head.stuckNormal == hit.normal);
                if (sticc)
                {
                    stuckNormal = hit.normal;
                    rb.AddForceAtPosition(stuckNormal * -stickyStrength, transform.position - transform.up * radius, ForceMode.Acceleration);
                }
                grounded = true;
            }
            onIce = (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ice"));

        }

        //rb.drag = rb.angularDrag = isStuck ? drag : 0;
        if (!isStuck && useGravity) {
            rb.AddForceAtPosition(Vector3.down * gravityStrength, transform.position - transform.up * radius, ForceMode.Acceleration);
        }

        mat.color = grounded ? Color.white : Color.red;
	}
}
