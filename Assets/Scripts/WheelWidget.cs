using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelWidget : Widget {

	public float rotationSpeed;

	private GameObject _player;

	private void Start() {
		_player = GameObject.FindGameObjectWithTag ("Player");
	}

	private void OnMouseDown() {
		StartCoroutine (FollowMouse());
	}

	Vector3 lastMouseCoord;
	private IEnumerator FollowMouse() {
		var diff = lastMouseCoord - Camera.main.ScreenToWorldPoint (Input.mousePosition);
		while(Input.GetMouseButton(0)) {
			var point = Camera.main.ScreenToWorldPoint (Input.mousePosition);// + diff;


			float AngleRad = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x);
			float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, AngleDeg), Time.deltaTime * rotationSpeed);
			_player.GetComponent<MovementBehaviour> ().targetRotation = transform.rotation;
			yield return null;
		}
		lastMouseCoord = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
}
