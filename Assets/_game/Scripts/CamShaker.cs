using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamShaker : MonoBehaviour
{

	Camera cam;

	public static CamShaker instance;
	private void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start()
	{
		cam = GetComponentInChildren<Camera>();
	}

	public void ShakeCountdown()
	{
		Shake();
	}

	public void ShakeStart()
	{
		Shake();
	}

	public void ShakePickup()
	{
		Shake();
	}

	void Shake( float duration = 0.5f, float intesity = 0.2f )
	{
		cam.DOShakePosition( duration, intesity );
	}
}