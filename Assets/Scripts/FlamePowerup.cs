using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePowerup : MonoBehaviour {
    public ParticleSystem system;

    public bool active;
    public AudioSource flame;

    public void Ignite() {
        active = true;
        var em = system.emission;
        em.enabled = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!active && collision.collider.CompareTag("Shroom")) {
            Ignite();
            flame.Play();
        }

        if (active) {
            var ice = collision.collider.GetComponent<IceMelt>();
            if (ice) {
                ice.melting = true;
            }
        }
    }
}
