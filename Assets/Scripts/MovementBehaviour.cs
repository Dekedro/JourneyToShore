using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {
	public Quaternion targetRotation;


	private SubmarineResource _food;
	private SubmarineResource _depth;
	private SubmarineResource _oxygen;
	private SubmarineResource _speed;
	private void Start() {
		_food = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubmarineBehaviour> ().Resources ["Food"];
		_depth = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubmarineBehaviour> ().Resources ["Depth"];
		_oxygen = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubmarineBehaviour> ().Resources ["Oxygen"];
		_speed = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubmarineBehaviour> ().Resources ["Speed"];
	}

	public void Update () {
		Sink ();
		Rotate ();
		Move ();
	}

	private void Sink() {

	}

	private void Rotate() {
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation,
			Time.deltaTime * (_food.Percentage >= 0.5f ? 1 : Mathf.Pow((_food.Percentage / 0.5f), 2))); 	
	}

	private void Move() {
		if (GetComponent<Rigidbody2D> ().velocity.magnitude < Mathf.Abs(_speed.Value)) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * _speed.Value);
		}

		var velocityMagnitude = GetComponent<Rigidbody2D> ().velocity.magnitude;
		var newVelocity = transform.up * velocityMagnitude;
		GetComponent<Rigidbody2D> ().velocity = new Vector3(newVelocity.x, newVelocity.y, 0);
	}
}
