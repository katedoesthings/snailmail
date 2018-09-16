using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBossFlame : MonoBehaviour {
    public float burnTime;

    private float remaining;
    private ParticleSystem.EmissionModule em;

	// Use this for initialization
	void Start () {
        em = GetComponent<ParticleSystem>().emission;
	}
	
	// Update is called once per frame
	void Update () {
		if (remaining > 0) {
            remaining -= Time.deltaTime;
            if (remaining <= 0) {
                em.enabled = false;
            }
        }
	}

    private void OnTriggerEnter(Collider collision) {
        var flame = collision.transform.root.GetComponentInChildren<FlamePowerup>();
        if (flame.active) {
            em.enabled = true;
            remaining = burnTime;
        }
    }
}
