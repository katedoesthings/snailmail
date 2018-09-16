using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePowerup : MonoBehaviour {
    public ParticleSystem system;

    private bool active;

    private void OnCollisionEnter(Collision collision) {
        if (!active && collision.collider.CompareTag("Shroom")) {
            active = true;
            var em = system.emission;
            em.enabled = true;
        }

        if (active) {
            var ice = collision.collider.GetComponent<IceMelt>();
            if (ice) {
                ice.melting = true;
            }
        }
    }
}
