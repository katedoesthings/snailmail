using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {
    public Vector3 target;
    private Vector3 start;

    public float time;

	// Use this for initialization
	void Start () {
        start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(start, target, Mathf.PingPong(Time.time, time) / time);
	}
}
