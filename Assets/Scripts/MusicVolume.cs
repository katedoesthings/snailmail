using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {
    public int version;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<SnailMovement>()) {
            MusicPlayer.inst.Enter(this);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<SnailMovement>()) {
            MusicPlayer.inst.Leave(this);
        }
    }
}
