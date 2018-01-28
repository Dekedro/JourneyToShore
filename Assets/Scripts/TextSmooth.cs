using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSmooth : MonoBehaviour {
	LinkedList<string> story;
	// Use this for initialization
	float scene_start;
	//float last_text_printed;
	bool stop = false;
	string now_writing_text;
	float writing_speed = 20;
	void Start () {
		scene_start = Time.time;
		//last_text_printed = scene_start;
		GetComponent<Text>().text = "";

		story = new LinkedList<string>();
		story.AddLast ("First text");
		story.AddLast ("Second text");
		story.AddLast ("Third text a lot a lot Third text a lot a lot Third text a lot a lot Third text a lot a lot Third text a lot a lot ");
		now_writing_text = "FIRST TEXT";
		StartCoroutine (Whatever ("FIRST TEXT"));//Whatever ();


		/*
		while (!(story.First == null)) {
			string t = story.First.Value;
			story.RemoveFirst ();
			StartCoroutine (Whatever(t));
		}
		*/

	}



	private IEnumerator Whatever(string s) {
		int n = 0;
		int t = 0;
		while (n < s.Length && !stop && s==now_writing_text) {
			//text += "a";
			if(t*writing_speed > n) {
				GetComponent<Text> ().text += s [n];
				n++;

			}
			t++;
			yield return null;
		}
		stop = true;
	}



	// Update is called once per frame
	void Update () {
		//GetComponent<Text> ().text;

		if (stop && Input.GetKeyDown("space")) {
			stop = false;
			GetComponent<Text>().text = "";
			if (story.First != null) {
				now_writing_text = story.First.Value;
				StartCoroutine (Whatever (story.First.Value));
				story.RemoveFirst ();
			} else {
				transform.parent.gameObject.SetActive(false);
				Time.timeScale = 1;
			}

		} 
			


	}


	void FixedUpdate() {
		//


		if (Input.GetKeyDown ("space")) {
			writing_speed = 0.8f;
		}
		if(Input.GetKeyUp("space")) {
			writing_speed = 10f;
		}//*/

	}


}
