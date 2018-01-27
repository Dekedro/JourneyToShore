using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayer : MonoBehaviour {
	public GameObject StatBars;

	private IDictionary<string, SubmarineResource> _resources;

	void Start () {
		_resources = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubmarineBehaviour> ().Resources;
		//Debug.Log (_resources ["Food"].Percentage);
		SetBounds();
	}
	
	void Update () {
		foreach (var statBar in StatBars.GetComponentsInChildren<Slider>()) {
			if(_resources.Keys.Contains(statBar.gameObject.name)) {
				Debug.Log (statBar.gameObject.name);
				statBar.value = _resources [statBar.gameObject.name].Value;
			}
		}
	}

	private void SetBounds() {
		foreach (var statBar in StatBars.GetComponentsInChildren<Slider>()) {
			if(_resources.Keys.Contains(statBar.gameObject.name)) {
				statBar.minValue = _resources [statBar.gameObject.name].LowerBound;
				statBar.maxValue = _resources [statBar.gameObject.name].UpperBound;
			}
		}
	}
}
