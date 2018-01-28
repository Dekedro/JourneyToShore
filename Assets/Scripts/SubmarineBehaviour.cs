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
		Resources.Add("Speed", new SubmarineResource(-50f, 50f, 1f, 0f, false));
		Resources.Add("Depth", new SubmarineResource (0f, 1000f, 100f, 0.01f, true));
		Resources.Add("Armor", new SubmarineResource (0f, 100f, 100f, 0.05f / Time.deltaTime, true));
		Resources ["Armor"].Cooldown = 1;
		SetConsumerFunctions ();
	}

	private void SetConsumerFunctions() {
		Resources ["Depth"].ConsumingAction = (resources, controlSliders, depthResource) => {
			float multiplier = (Random.Range(1, 20) == 1 ? 20 : 1); 

			if(Random.Range(1, 10) % 3 == 0) {
				multiplier *= 1f + resources["Oxygen"].Percentage;
				multiplier *= 1f + (1f - resources["Food"].Percentage);
				//randomly go up, to create spiking effect.
				return depthResource.Value - Random.Range(depthResource.ValueConsumedPerSecond * depthResource.ConsumeMultiplyer * Time.deltaTime,
					depthResource.ValueConsumedPerSecond * depthResource.ConsumeMultiplyer * Time.deltaTime * 5 * multiplier);
			} else {
				multiplier *= 1f + (1f - resources["Oxygen"].Percentage);
				return depthResource.Value + Random.Range(depthResource.ValueConsumedPerSecond * depthResource.ConsumeMultiplyer * Time.deltaTime,
					depthResource.ValueConsumedPerSecond * depthResource.ConsumeMultiplyer * Time.deltaTime * 7 * multiplier);
			}
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