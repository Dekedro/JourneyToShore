using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
	public GameObject menu;

	bool game_paused = false;
	public void Pause() {
		if (game_paused) {
			Time.timeScale = 1;
			menu.SetActive (false);
			game_paused = false;
		} else {
			Time.timeScale = 0;	
			menu.SetActive (true);
			game_paused = true;
		}
	}
}
