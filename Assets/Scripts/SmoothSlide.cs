using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothSlide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject centre;

	void FixedUpdate() {
		// to 0 and -77
		transform.position = Vector3.Lerp(transform.position, centre.transform.position, 0.05f);
	}
}
