using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLength : MonoBehaviour {
    private Material mat;
    private TrailRenderer trail;

	// Use this for initialization
	void Start () {
        trail = GetComponent<TrailRenderer>();
        mat = trail.material;
	}
	
	// Update is called once per frame
	void Update () {
        float length = 0;

        for (int i = 0; i < trail.positionCount - 1; i++) {
            length += Vector3.Distance(trail.GetPosition(i), trail.GetPosition(i + 1));
        }



        mat.mainTextureScale = new Vector3(length / 4, 1);
        mat.mainTextureOffset = new Vector3(-length / 4, 1);
    }
}
