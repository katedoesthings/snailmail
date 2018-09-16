using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLeaf : MonoBehaviour {
    public static int numGot;
    public AudioClip sound;

	// Use this for initialization
	void Start () {
        numGot = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
	}

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        numGot++;
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
