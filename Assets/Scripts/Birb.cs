using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birb : MonoBehaviour {
    public float distance;
    public float down;
    public float cycleTime = 5;
    public AudioSource caw;
    
    private Vector3 v1;
    private Vector3 v2;
    private Vector3 v3;

    private Vector3 f1;
    private Vector3 f2;

	// Use this for initialization
	void Start () {
        v1 = transform.position;
        v2 = transform.position + transform.forward * distance * 0.5f - transform.up * down;
        v3 = transform.position + transform.forward * distance;

        f1 = transform.forward;
        f2 = -transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        float progress = Time.time % cycleTime;

        if (progress < cycleTime * 0.25f) {
            transform.position = Vector3.Lerp(v1, v2, progress * 4.0f / cycleTime);
            transform.forward = f1;
        } else if (progress < cycleTime * 0.5f) {
            transform.position = Vector3.Lerp(v2, v3, progress * 4.0f / cycleTime - 1.0f);
            transform.forward = f1;
            if (!caw.isPlaying)
            {
                caw.Play();
            }
            
        } else if (progress < cycleTime * 0.75f) {
            transform.position = Vector3.Lerp(v3, v2, progress * 4.0f / cycleTime - 2.0f);
            transform.forward = f2;
        } else {
            transform.position = Vector3.Lerp(v2, v1, progress * 4.0f / cycleTime - 3.0f);
            transform.forward = f2;
        }
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.GetComponent<SnailSection>()) {
            Checkpoint.active?.Respawn();
        }
    }
}
