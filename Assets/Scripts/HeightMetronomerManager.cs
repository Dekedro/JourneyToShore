using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMetronomerManager : MonoBehaviour {
	private SubmarineResource _depth;
	private HeightMetronomerPaint _grapher;

	private void Start () {
		_depth = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubmarineBehaviour> ().Resources ["Depth"];
		_grapher = GetComponent<HeightMetronomerPaint> ();
		StartCoroutine (DrawGrapher ());
	}
	
	private IEnumerator DrawGrapher() {
		while (true) {
			_grapher.DrawHeight (_depth.Value);
			yield return null;
		}
	}
}
