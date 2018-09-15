using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour {
    private SnailSection ss;

    public float velocity;

    public SnailSection[] bodySections;

	// Use this for initialization
	void Start () {
        ss = GetComponent<SnailSection>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 f = Camera.main.transform.forward;
        if (ss.isStuck) {
            f = Vector3.ProjectOnPlane(f, ss.gravityNormal);
        }
        f = f.normalized;
        
        Vector3 v = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * f;
        
        ss.rb.AddForce(v * velocity, ForceMode.Acceleration);


        if (Input.GetButton("Jump")) {
            ss.sticky = bodySections[0].sticky = false;
            ss.rb.AddForce(Vector3.up * 50);
        } else {
            ss.sticky = bodySections[0].sticky = true;
        }
        
        var lastSection = ss;
        var lastPos = ss.rb.transform.position - ss.rb.transform.forward * 0.5f;
        var lastUp = ss.rb.transform.up;

        foreach (var section in bodySections) {
            var curPosOnSection = section.transform.position + section.transform.forward * 0.5f;
            section.rb.AddForceAtPosition((lastPos - curPosOnSection) * 100, curPosOnSection);
            lastSection.rb.AddForceAtPosition((curPosOnSection - lastPos) * 100, lastPos);

            var curUp = section.transform.up;
            section.rb.AddTorque(Vector3.Cross(curUp, lastUp) * 10);

            Debug.DrawLine(curPosOnSection, lastPos);

            lastPos = section.transform.position - section.transform.forward * 0.5f;
            lastSection = section;
            lastUp = curUp;


            section.rb.velocity = Vector3.Project(section.rb.velocity, section.transform.forward);
        }
        
	}
}
