using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarinOnCollider : MonoBehaviour {

	public GameObject WinPrompt;

	public void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.gameObject.name + " " + other.gameObject.tag);
		//Destroy	(other.gameObject);
		if (other.gameObject.CompareTag("Port")) {
			WinPrompt.SetActive (true);
			Time.timeScale = 0;
		}
	}
}
