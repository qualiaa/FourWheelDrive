using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticle : MonoBehaviour {
	private const float rotationSpeed = 0.03f;
	Wheel wheel;
	Rigidbody rb;
	public ParticleSystem motes;
	public ParticleSystem clouds;

	// Use this for initialization
	void Start () 
	{
		wheel = GetComponentInParent<Wheel>();
		rb = GetComponentInParent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		SetEmissionRate( clouds, 10f );
		SetEmissionRate( motes, 1f );
		transform.Rotate( Vector3.right * wheel.power * rotationSpeed * Time.deltaTime );
	}

	private void SetEmissionRate( ParticleSystem ps, float multiplier )
	{
		ParticleSystem.EmissionModule emissionMod = ps.emission;

		emissionMod.rateOverTime = GetParticleEmission() * multiplier;
	}

	private float GetParticleEmission()
	{
		float dot = Vector3.Dot( wheel.totalForce.normalized, rb.velocity.normalized );
		dot *= wheel.power;
		if( dot > 0f )
		{
			return 1f;
		}
		else{
			return Mathf.Lerp( 0, 4, Mathf.Abs( dot ) );
		}

	}
}
