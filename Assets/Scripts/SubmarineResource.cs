using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SubmarineResource {

	public float UpperBound { get; set; }
	public float LowerBound { get; set; }
	public float Value { get; set; }
	public float PercentConsumedPerSecond { get; set; }
	public float ValueConsumedPerSecond { get; }
	public bool IsBeingConsumed { get; set; }
	public float ConsumeMultiplyer { get; set; }
	public float Cooldown { get; set; }
	public Func<IDictionary<string, SubmarineResource>, IDictionary<string, Slider>, SubmarineResource, float> ConsumingAction { get; set; }

	private float _lastTimeConsumed;

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
		ValueConsumedPerSecond = UpperBound * PercentConsumedPerSecond;
		ConsumeMultiplyer = 1;
		Cooldown = 0;
	}

	public void Consume(IDictionary<string, SubmarineResource> resources, IDictionary<string, Slider> controls) {
		if (IsBeingConsumed && Time.time > _lastTimeConsumed + Cooldown) {
			if (ConsumingAction == null) {
				if (Value - ValueConsumedPerSecond * ConsumeMultiplyer * Time.deltaTime >= 0)
					Value -= ValueConsumedPerSecond * ConsumeMultiplyer * Time.deltaTime;
				else
					Value = 0;
			} else {
				Value = ConsumingAction(resources, controls, this);
			}
			_lastTimeConsumed = Time.time;
		}
	}
}
