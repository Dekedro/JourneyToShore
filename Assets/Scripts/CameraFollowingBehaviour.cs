using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingBehaviour : MonoBehaviour {
	public GameObject target;

	public float trackingSpeed = 1f;
	private Vector3 _velocity = Vector3.zero;
	private void Update () {
		var destination = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, trackingSpeed); 
	}
}
