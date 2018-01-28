using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void WhenPressed() {
		Application.LoadLevel ("Game");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
