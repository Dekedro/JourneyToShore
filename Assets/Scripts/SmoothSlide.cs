using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothSlide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("ScriptableObject started");
		StartCoroutine (Transit () );

	}


	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject centre;

	void Go() {
		// to 0 and -77
		transform.position = Vector3.Lerp (transform.position, centre.transform.position, 0.1f);
	}

	private IEnumerator Transit(){
		int n = 0;
		while (Vector3.Distance (transform.position, centre.transform.position) >= 0.01) {
			Go ();
			yield return null;
		}
	}
}
