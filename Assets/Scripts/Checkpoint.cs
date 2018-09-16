﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public static Checkpoint active { get; set; }

    public GameObject PlayerPrefab;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<SnailMovement>()) {
            active = this;
        }
    }

    public void Respawn() {
        Destroy(FindObjectOfType<SnailMovement>().transform.root.gameObject);
        var np = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        FindObjectOfType<CameraControl>().track = np.GetComponentInChildren<SnailMovement>().transform;
    }
}
