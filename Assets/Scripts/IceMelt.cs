using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMelt : MonoBehaviour {
    private MeshRenderer mr;
    private Material mat;
    private Collider col;
    public float meltTime;
    public float curAmount;


    public bool melting;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        mat = mr.material;
        curAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (melting) {
            curAmount -= Time.deltaTime / meltTime;
            curAmount = Mathf.Clamp01(curAmount);
            mat.SetFloat("_Cutoff", 1 - curAmount);

            if (curAmount == 0) {
                melting = false;
                col.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
	}
}
