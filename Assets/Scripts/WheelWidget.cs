using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelWidget : Widget {

	public float rotationSpeed;

	private GameObject _player;

	private void Start() {
		_player = GameObject.FindGameObjectWithTag ("Player");
		rotationSpeed = 0.1f;
	}

	private void OnMouseDown() {
		StartCoroutine (FollowMouse());
	}

	Vector3 lastMouseCoord;
	private IEnumerator FollowMouse() {
		var diff = lastMouseCoord - Camera.main.ScreenToWorldPoint (Input.mousePosition);
		bool initialized = false;
		float startAngle = 0;
		float rotated = 0;
	
		float lastAngle = 0;
		while(Input.GetMouseButton(0) && System.Math.Abs(rotated) < 180) {
			if (!initialized) {
				initialized = true;
				var point = Camera.main.ScreenToWorldPoint (Input.mousePosition);// + diff;


				float AngleRad = Mathf.Atan2 (point.y - transform.position.y, point.x - transform.position.x);
				float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
				startAngle = AngleDeg;
			} else {
				var point = Camera.main.ScreenToWorldPoint (Input.mousePosition);// + diff;


				float AngleRad = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x);
				float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
			
				startAngle += 0.1f*(AngleDeg-startAngle);
				if (System.Math.Abs (AngleDeg - startAngle) > 0.1) {
					transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (AngleDeg - startAngle)*rotationSpeed);
					rotated += (AngleDeg - startAngle) * rotationSpeed;
					lastAngle = AngleDeg;
				}

					
				_player.GetComponent<MovementBehaviour> ().targetRotation = transform.rotation;

			}
			yield return null;
		}
		while (!Input.GetMouseButton (0) && startAngle != lastAngle) {
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (lastAngle - startAngle)*rotationSpeed);
			startAngle += 0.1f*(lastAngle-startAngle);
			yield return null;
		}
		lastMouseCoord = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
}
