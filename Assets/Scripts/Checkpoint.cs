﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public static Checkpoint active { get; set; }

    public GameObject PlayerPrefab;
    public AudioSource sound;

    private void Awake() {
        active = null;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<SnailMovement>()) {
            active = this;
            sound.Play();
        }
    }

    public void Respawn() {
        bool hadFlame = FindObjectOfType<FlamePowerup>().active;
        Destroy(FindObjectOfType<SnailMovement>().transform.root.gameObject);
        var np = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        FindObjectOfType<CameraControl>().track = np.GetComponentInChildren<SnailMovement>().transform;

        if (hadFlame) {
            FindObjectOfType<FlamePowerup>().Ignite();
        }
    }
}
