using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour {
    private Rigidbody rb;

    public float velocity;

    public Rigidbody[] bodySections;

    public float gravityStrength = 9.8f;

    private Vector3 gravityNormal;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gravityNormal = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 f = Camera.main.transform.forward;
        f.y = 0;
        f = f.normalized;

        Vector3 v = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * f;

        rb.AddForce(v * velocity, ForceMode.Acceleration);
        rb.AddForce(gravityNormal * -gravityStrength, ForceMode.Acceleration);


        if (Input.GetButton("Jump")) {
            rb.AddForce(Vector3.up * 12);
        }
        
        var lastSection = rb;
        var lastPos = rb.transform.position - rb.transform.forward * 0.5f;
        var lastUp = rb.transform.up;

        foreach (var section in bodySections) {
            var curPosOnSection = section.transform.position + section.transform.forward * 0.5f;
            section.AddForceAtPosition((lastPos - curPosOnSection) * 100, curPosOnSection);
            lastSection.AddForceAtPosition((curPosOnSection - lastPos) * 100, lastPos);

            var curUp = section.transform.up;
            section.AddTorque(Vector3.Cross(curUp, lastUp) * 10);
            section.AddForce(gravityNormal * -gravityStrength, ForceMode.Acceleration);

            Debug.DrawLine(curPosOnSection, lastPos);

            lastPos = section.transform.position - section.transform.forward * 0.5f;
            lastSection = section;
            lastUp = curUp;


            section.velocity = Vector3.Project(section.velocity, section.transform.forward);
        }
        
	}
}
