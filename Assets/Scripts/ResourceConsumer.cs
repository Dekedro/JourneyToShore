using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceConsumer : MonoBehaviour {

	public bool consumeResources = true;

	private IDictionary<string, Slider> _controlSliders { 
		get { 
			var sliders = GameObject.Find ("ControllerMonitor").GetComponentsInChildren<Slider> (); 
			var dict = new Dictionary<string, Slider> ();
			foreach (var slider in sliders) { 
				dict.Add (slider.gameObject.name, slider);
			}
			return dict;
		}
	}

	private IDictionary<string, SubmarineResource> _resources;


	private void Start () {
		_resources = GetComponent<SubmarineBehaviour>().Resources;		
	}
	
	private void Update () {
		foreach (var resource in _resources.Keys) {
			if(resource != "Armor")
				_resources[resource].Consume (_resources, _controlSliders);
		}
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.name == "Border") {
			_resources ["Armor"].Consume (_resources, _controlSliders);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag ("RechargingArea")) {
			var rechargingArea = collider.gameObject.GetComponent<RechargingArea> ();
			foreach (var rechargable in rechargingArea.RechargableResources) {
				_resources [rechargable].Recharge ();
			}
		}
	}
}
