using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour {

	public GameObject v;
	public void PressedDown() {
		v.SetActive (false);
		Time.timeScale = 1;
	}
}
