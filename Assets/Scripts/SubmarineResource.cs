using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineResource {

	public float UpperBound { get; set; }
	public float LowerBound { get; set; }
	public float Value { get; set; }
	public float PercentConsumedPerSecond { get; set; }
	public bool IsBeingConsumed { get; set; }

	public float Percentage { 
		get 
		{
			return Value / (UpperBound - LowerBound);
		}
	}

	public SubmarineResource() : this(0f, 100f, 100f, 0.01f, true) {}

	public SubmarineResource(float lowerBound, float upperBound, float value, float percentConsumedPerSecond, bool isBeingConsumed) {
		LowerBound = lowerBound;
		UpperBound = upperBound;
		Value = value;
		PercentConsumedPerSecond = percentConsumedPerSecond;
		IsBeingConsumed = isBeingConsumed;
	}

	public void Consume() {
		Value -= Value * PercentConsumedPerSecond * Time.deltaTime;
	}
}
