﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour {

	public ParticleSystem _particles;
	void Start () {
		//_particles = GetComponentInChildren<ParticleSystem> ();
	}
	
	public void FireSonar() {
		_particles.Emit (100);
	}
}
