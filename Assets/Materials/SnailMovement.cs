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

    public int climbSections = 3;

    public Transform[] eyeStalks;

	// Use this for initialization
	void Start () {
        ss = GetComponent<SnailSection>();
	}
	
	// Update is called once per frame
	void Update () {
        var firstStuck = bodySections.FirstOrDefault(s => s.isStuck);


        Vector3 f = Camera.main.transform.forward;
        Vector3 u = Camera.main.transform.up;
        if (firstStuck) {
            f = Vector3.ProjectOnPlane(f, firstStuck.stuckNormal);
            u = Vector3.ProjectOnPlane(u, firstStuck.stuckNormal);
        }

        if (u.sqrMagnitude > f.sqrMagnitude) {
            
            f = u;
        }

        f = f.normalized;
        
        Vector3 v = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * f;

        if (!firstStuck) {
            v *= 0.5f;
        }

        ss.rb.AddForce(v * velocity, ForceMode.Acceleration);

        bool headUp = firstStuck && Input.GetButton("Jump");
        if (headUp) {
            for (int i = 0; i < headSections; i++) {
                bodySections[i].sticky = false;
                bodySections[i].useGravity = false;
                //bodySections[i].rb.AddForce(v * velocity, ForceMode.Acceleration);
            }
            ss.sticky = false;
            ss.useGravity = false;
            Vector3 force = firstStuck.stuckNormal * 10 + firstStuck.transform.forward * 5;
            force += firstStuck.stuckNormal * v.magnitude * 60;
            ss.rb.AddForce(force);
        } else {
            ss.sticky = true;
            ss.useGravity = true;
            for (int i = 0; i < headSections; i++) {
                bodySections[i].sticky = true;
                bodySections[i].useGravity = true;
            }
        }

        if (firstStuck) {
            var firstSuckUp = firstStuck.stuckNormal;
            ss.rb.AddTorque(Vector3.Cross(ss.transform.up, ss.isStuck ? ss.stuckNormal : Vector3.ProjectOnPlane(firstSuckUp, ss.transform.forward)) * 10);
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
            if (section.sticky && !section.isStuck && section.potentialGround.sqrMagnitude > 0) {
                lastUp = section.potentialGround;
            }
            section.rb.AddTorque(Vector3.Cross(curUp, ss.isStuck ? ss.stuckNormal : Vector3.ProjectOnPlane(lastUp, section.transform.forward)) * 10);

            Debug.DrawLine(curPosOnSection, lastPos);

            lastPos = section.transform.position - section.transform.forward * section.radius;
            lastSection = section;
            lastUp = curUp;

            if (section.isStuck) {
                section.rb.velocity = Vector3.Project(section.rb.velocity, section.transform.forward);
            }
        }
        
        Vector3 eyeUp = firstStuck?.transform.up ?? Vector3.up;
        Vector3 eyeFwd = firstStuck?.transform.forward ?? transform.forward;
        Vector3 eyeRight = Vector3.Cross(eyeUp, eyeFwd);
        eyeFwd = Vector3.Cross(eyeRight, eyeUp);

        Quaternion q = Quaternion.LookRotation(eyeRight, -eyeFwd);
        eyeStalks[0].transform.rotation = Quaternion.RotateTowards(eyeStalks[0].transform.rotation, q, Time.deltaTime * 360);


        Quaternion q2 = Quaternion.LookRotation(-eyeFwd, -eyeRight);
        eyeStalks[1].transform.rotation = Quaternion.RotateTowards(eyeStalks[1].transform.rotation, q2, Time.deltaTime * 360);

    }

    private void LateUpdate() {
        for (int i = bodySections.Length - 1; i >= 0; i--) {
            var bs = bodySections[i];
            var mn = bs.meshNode;
            
            if (mn) {
                mn.transform.position = bs.transform.position;
                mn.transform.rotation = bs.transform.rotation * bs.quatToApply;
            }
        }

        if (ss.meshNode) {
            ss.meshNode.transform.position = ss.transform.position;
            ss.meshNode.transform.rotation = ss.transform.rotation * ss.quatToApply;
        }
    }
}
