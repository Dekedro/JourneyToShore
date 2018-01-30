using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlidingBehaviour : MonoBehaviour {
	public Vector3 targetPosition;

	public bool useLinearInterpolation;
	public AnimationCurve movementCurve;

	private Vector3 _startingPosition;
	private bool _isAtOrigin = true;
	private bool _movementPaused = false;
	private Action<bool> _callback = null;

	private void Awake() {
		_startingPosition = transform.localPosition;
	}
		
	public void Toggle() {
		if (_isAtOrigin) {
			MoveToTarget ();
		} else {
			MoveToOrigin ();
		}
	}

	public void TeleportToTarget() {
		if (_isAtOrigin) {
			StopAllCoroutines();
			transform.localPosition = targetPosition;
			_isAtOrigin = false;
		}
	}

	public void TeleportToOrigin() {
		if (!_isAtOrigin) {
			StopAllCoroutines();
			transform.localPosition = _startingPosition;
			_isAtOrigin = true;
		}
	}

	public void MoveToTarget() 
	{
		if (_isAtOrigin) {
			StopAllCoroutines();
			StartCoroutine(Move(transform.localPosition, targetPosition, false));
		}
	}

	public void MoveToOrigin() 
	{
		if (!_isAtOrigin) {
			StopAllCoroutines();
			StartCoroutine(Move(transform.localPosition, _startingPosition, true));
		}

	}

	private IEnumerator Move(Vector2 startPos, Vector2 targetPos, bool willBeAtOrigin) {
		var startTime = Time.time;
		while (((Vector2)transform.localPosition - targetPos).magnitude > Time.deltaTime) {

			var timePauseStarted = Time.time;
			while (_movementPaused) {
				yield return null;
			}
			startTime += Time.time - timePauseStarted;

			if (useLinearInterpolation) {
				transform.localPosition = Vector3.Lerp(startPos, targetPos, Time.time - startTime);
			} else {
				var currentTime = Time.time - startTime;
				var progress = Utils.Remap(
					movementCurve.Evaluate(currentTime),
					movementCurve.keys[0].value,
					movementCurve.keys[movementCurve.length-1].value,
					0,
					1
				);
				transform.localPosition = new Vector3(
					Utils.Remap(progress, 0, 1, startPos.x, targetPos.x),
					Utils.Remap(progress, 0, 1, startPos.y, targetPos.y));
			}

			transform.localPosition = new Vector3(
				transform.localPosition.x,
				transform.localPosition.y,
				_startingPosition.z);
			yield return null;
		}
		_isAtOrigin = willBeAtOrigin;
		if(_callback != null)
			_callback(_isAtOrigin);
	}

	/// <summary>
	/// Sets the callback.
	/// </summary>
	/// <param name="action">Action which takes a bool "isMovingToOrigin"</param>
	public void SetCallback(Action<bool> action) {
		_callback = action;
	}

	public void Pause() {
		_movementPaused = true;
	}

	public void Unpause() {
		_movementPaused = false;
	}
}