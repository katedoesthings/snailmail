using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnailMovement : MonoBehaviour {
    private SnailSection ss;

    public float velocity;

    public SnailSection[] bodySections;
    public float connectionStrength = 200;

    public int headSections = 2;

    public Transform[] eyeStalks;

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

        var firstStuck = bodySections.FirstOrDefault(s => s.isStuck);

        bool up = Input.GetButton("Jump") && firstStuck;
        if (up) {
            for (int i = 0; i < headSections; i++) {
                bodySections[i].sticky = false;
                bodySections[i].useGravity = false;
                //bodySections[i].rb.AddForce(v * velocity, ForceMode.Acceleration);
            }
            ss.sticky = false;
            ss.useGravity = false;
            ss.rb.AddForce(firstStuck.gravityNormal * 40);
        } else {
            ss.sticky = true;
            ss.useGravity = true;
            for (int i = 0; i < headSections; i++) {
                bodySections[i].sticky = true;
                bodySections[i].useGravity = true;
            }
        }

        if (firstStuck) {
            var u = firstStuck.gravityNormal;
            ss.rb.AddTorque(Vector3.Cross(ss.transform.up, ss.isStuck ? ss.gravityNormal : Vector3.ProjectOnPlane(u, ss.transform.forward)) * 10);
        }


        
        var lastSection = ss;
        var lastPos = ss.rb.transform.position - ss.rb.transform.forward * ss.radius;
        var lastUp = ss.rb.transform.up;

        for (int i = 0; i < bodySections.Length; i++) {
            var section = bodySections[i];
            var curPosOnSection = section.transform.position + section.transform.forward * section.radius;
            section.rb.AddForceAtPosition((lastPos - curPosOnSection) * connectionStrength, curPosOnSection);
            lastSection.rb.AddForceAtPosition((curPosOnSection - lastPos) * connectionStrength, lastPos);

            var curUp = section.transform.up;

            section.rb.AddTorque(Vector3.Cross(curUp, ss.isStuck ? ss.gravityNormal : Vector3.ProjectOnPlane(lastUp, section.transform.forward)) * 10);

            Debug.DrawLine(curPosOnSection, lastPos);

            lastPos = section.transform.position - section.transform.forward * section.radius;
            lastSection = section;
            lastUp = curUp;

            if (section.isStuck) {
                section.rb.velocity = Vector3.Project(section.rb.velocity, section.transform.forward);
            }
        }

        foreach (var stalk in eyeStalks) {
            stalk.transform.right = -(firstStuck?.gravityNormal ?? Vector3.up);
        }
        
	}
}
