using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConsumer : MonoBehaviour {

	public bool consumeResources = true;

	private IDictionary<string, SubmarineResource> _resources;

	private void Start () {
		_resources = GetComponent<SubmarineBehaviour>().Resources;		
	}
	
	private void Update () {
		foreach (var resource in _resources.Keys) {
			_resources [resource].Consume ();
		}
	}
}
