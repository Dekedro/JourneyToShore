using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelWidget : Widget {

	public float rotationSpeed;

	private GameObject _player;

	private void Start() {
		_player = GameObject.FindGameObjectWithTag ("Player");
		rotationSpeed = 0.05f;
	}

	private void OnMouseDown() {
		StartCoroutine (FollowMouse());
	}

	//float rotationSpeed;

	Vector3 lastMouseCoord;
	private IEnumerator FollowMouse() {
		Debug.Log ("nueina");
		var diff = lastMouseCoord - Camera.main.ScreenToWorldPoint (Input.mousePosition);
		bool initialized = false;
		float startAngle = 0;
	
		while(Input.GetMouseButton(0)) {
			if (!initialized) {
				initialized = true;
				var point = Camera.main.ScreenToWorldPoint (Input.mousePosition);// + diff;


				float AngleRad = Mathf.Atan2 (point.y - transform.position.y, point.x - transform.position.x);
				float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
				startAngle = AngleDeg;
				//Debug.Log (startAngle);
			} else {
				var point = Camera.main.ScreenToWorldPoint (Input.mousePosition);// + diff;


				float AngleRad = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x);
				float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
				//Debug.Log (AngleDeg);
				Debug.Log (transform.rotation.z);
				Debug.Log (AngleDeg - startAngle);
				if (System.Math.Abs(AngleDeg - startAngle) > 0.1)
					transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (AngleDeg - startAngle)*rotationSpeed);
				//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, AngleDeg), Time.deltaTime * rotationSpeed);
				_player.GetComponent<MovementBehaviour> ().targetRotation = transform.rotation;

			}
			yield return null;
		}
		lastMouseCoord = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
}
