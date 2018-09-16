using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSound : MonoBehaviour {
    public AudioSource start;
    public AudioSource loop;
    public AudioSource end;
    public SnailSection icer;
  
	// Use this for initialization
	void Start () {
        /*AudioSource[] aSources = GetComponents<AudioSource>();
        start = aSources[3];
        loop = aSources[1];
        end = aSources[2];*/
       
	}
	
	// Update is called once per frame
	void Update () {
        /*if(Input.GetButtonDown("w") || Input.GetButtonDown("s") ||
            Input.GetButtonDown("a") || Input.GetButtonDown("d"))
        {
            start.Play();
            looped = true;
        }*/
        /*if(!loop.isPlaying)
            loop.Play();
            */
        if(Input.GetKey("w") || Input.GetKey("a") ||
            Input.GetKey("s") || Input.GetKey("d")){
            if (!loop.isPlaying && !icer.onIce)
            {
                loop.Play();
            }
            else if(!loop.isPlaying && icer.onIce && !end.isPlaying)
            {
                end.Play();
            }

        }
		/*if(Input.GetButtonUp("w") || Input.GetButtonUp("s") ||
            Input.GetButtonUp("a") || Input.GetButtonUp("d"))
        {
            looped = false;
            end.Play();
        }*/
	}
}
