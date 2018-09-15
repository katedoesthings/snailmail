using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnailDeath : MonoBehaviour {
    public float scaleRate;
    public float minScale;

    public bool ded;

    public ParticleSystem ps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ded) {
            var em = ps.emission;
            em.enabled = true;

            float y = transform.localScale.y;
            y -= scaleRate * Time.deltaTime;
            y = Mathf.Clamp(minScale, y, 1);
            transform.localScale = new Vector3(1, y, 1);

            foreach (var rb in transform.root.GetComponentsInChildren<Rigidbody>()) rb.isKinematic = true;
        }
	}
}
