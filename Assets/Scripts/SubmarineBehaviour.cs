using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineBehaviour : MonoBehaviour {
	public IDictionary<string, SubmarineResource> Resources { get; set; }

	private void Awake() {
		Resources = new Dictionary<string, SubmarineResource> ();
		Resources.Add("Fuel", new SubmarineResource());
		Resources.Add("Food", new SubmarineResource());
		Resources.Add("Oxygen", new SubmarineResource());
		Resources.Add("Depth", new SubmarineResource (0f, 1000f, 100f, -0.02f, true));
	}

	private void Start () {
		
	}
}