using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBehaviour : MonoBehaviour {
	public GameObject Controllers;

	private IDictionary<string, Slider> _controllers;
	private IDictionary<string, SubmarineResource> _resources { 
		get
		{
			return GameObject.FindGameObjectWithTag("Player").GetComponent<SubmarineBehaviour> ().Resources;
		}
	}

	void Start () {
		_controllers = new Dictionary<string, Slider> ();
		foreach (var slider in Controllers.GetComponentsInChildren<Slider>()) {
			_controllers.Add(slider.gameObject.name, slider);
		}

		
		ConfigureSpeedController ();
		SetBounds ();
	}

	private void SetBounds() {
		foreach (var sliderName in _controllers.Keys) {
			if (sliderName != "Speed") {
				_controllers[sliderName].minValue = 0;
				_controllers[sliderName].maxValue = 1;
				_controllers [sliderName].value = 0;
			}
		}
	}

	public void ConfigureSpeedController() {
		var slider = _controllers["Speed"];

		_resources ["Speed"].Value = _resources ["Speed"].UpperBound * slider.value;

		_resources ["Fuel"].ConsumeMultiplyer = 1f + Mathf.Abs(slider.value);
		_resources ["Battery"].ConsumeMultiplyer = 1f + Mathf.Abs(slider.value);
	}

	private bool isUsingBattery = false;
	public void ToggleBatteryFuelUsage() {
		if (isUsingGenerator) {
			_controllers ["Generator"].value = 0f;
		}

		if (!isUsingBattery) {
			_resources ["Fuel"].IsBeingConsumed = false;
			_resources ["Oxygen"].ConsumeMultiplyer = 1f;
			_resources ["Battery"].IsBeingConsumed = true;
		} else {
			_resources ["Fuel"].IsBeingConsumed = true;
			_resources ["Oxygen"].ConsumeMultiplyer = 1.5f;
			_resources ["Battery"].IsBeingConsumed = false;
		}
		isUsingBattery = !isUsingBattery;
	}

	private bool isUsingGenerator = false;
	public void ToggleGenerator() {
		if (isUsingBattery) {
			_controllers ["Switch"].value = 0f;
		}

		if (!isUsingGenerator) {
			_resources ["Battery"].IsBeingConsumed = true;
			_resources ["Fuel"].ConsumeMultiplyer -= 0.25f;
			_resources ["Battery"].ConsumeMultiplyer = -1;
		} else {
			_resources ["Battery"].IsBeingConsumed = false;
			_resources ["Fuel"].ConsumeMultiplyer += 0.25f;
			_resources ["Battery"].ConsumeMultiplyer = 1;
		}
		isUsingGenerator = !isUsingGenerator;
	}
}
