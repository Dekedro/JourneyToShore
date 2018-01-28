using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SubmarineBehaviour : MonoBehaviour {
	public IDictionary<string, SubmarineResource> Resources { get; set; }

	private void Awake() {
		Resources = new Dictionary<string, SubmarineResource> ();
		Resources.Add("Fuel", new SubmarineResource());
		Resources.Add("Battery", new SubmarineResource());
		Resources ["Battery"].IsBeingConsumed = false;
		Resources.Add("Food", new SubmarineResource());
		Resources.Add("Oxygen", new SubmarineResource());
		Resources ["Oxygen"].ConsumeMultiplyer = 1.5f; //Because starts off using engine instead of battery
		Resources.Add("Speed", new SubmarineResource(-3f, 3f, 1f, 0f, false));
		Resources.Add("Depth", new SubmarineResource (0f, 1000f, 100f, 0.01f, true));
		SetConsumerFunctions ();
	}

	private void SetConsumerFunctions() {
		Resources ["Depth"].ConsumingAction = (resources, controlSliders, depthResource) => {
			if(depthResource.IsBeingConsumed) {
				if(resources["Oxygen"].Percentage >= 0.7f)
					return depthResource.Value + depthResource.ValueConsumedPerSecond * depthResource.ConsumeMultiplyer * Time.deltaTime;
				else if(resources["Oxygen"].Percentage < 0.5f)
					return depthResource.Value - depthResource.ValueConsumedPerSecond * depthResource.ConsumeMultiplyer * Time.deltaTime;
			}
			return depthResource.Value;
		};

		Resources ["Fuel"].ConsumingAction = (resources, controlSliders, fuelResource) => {
			var percentage = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / resources["Speed"].UpperBound;

			if(resources["Speed"].Value == 0 || percentage < 0.01f) {
				return fuelResource.Value;
			} else {
				return fuelResource.Value - fuelResource.ValueConsumedPerSecond * fuelResource.ConsumeMultiplyer * Time.deltaTime;
			}
		};

		Resources ["Battery"].ConsumingAction = Resources ["Fuel"].ConsumingAction;
	}
}