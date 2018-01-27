using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {
	public float rotationSpeed;
	public float moveSpeed;
	public Quaternion targetRotation;

	public void Update () {
		Rotate ();
		Move ();
	}

	private void Rotate() {
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); 	
	}

	private void Move() {
		if (GetComponent<Rigidbody2D> ().velocity.magnitude < moveSpeed) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * moveSpeed);
		}

		var velocityMagnitude = GetComponent<Rigidbody2D> ().velocity.magnitude;
		//GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		var newVelocity = transform.up * velocityMagnitude;
		GetComponent<Rigidbody2D> ().velocity = new Vector3(newVelocity.x, newVelocity.y, 0);
	}
}
