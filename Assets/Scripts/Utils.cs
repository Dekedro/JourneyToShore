using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class Utils {
	public static IEnumerator WaitForSecondsAndExecute(float secondsToWait, Action callback) {
		yield return new WaitForSeconds(secondsToWait);
		callback();
	}

	public static IEnumerator WrapEnumeratorWithCallback(IEnumerator enumerator, Action callback) {
		yield return enumerator;
		callback();
	}
		
	public static float Remap(float value, float initialRangeMinimum, float initialRangeMaximum, float resultRangeMinimum, float resultRangeMaximum) {
		return (value - initialRangeMinimum) / (initialRangeMaximum - initialRangeMinimum) * (resultRangeMaximum - resultRangeMinimum) + resultRangeMinimum;
	}
}