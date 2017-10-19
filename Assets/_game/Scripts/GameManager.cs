using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public TMP_Text gameTimer;
	float timeToEndGame;
	PickupManager pickupManager;

	// Use this for initialization
	void Start () 
	{
		pickupManager = GetComponent<PickupManager>();

		timeToEndGame = Time.time + 60f;
		pickupManager.StartSpawning();
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameTimer.SetText( SplashJam.Utils.GetSecondsAndMinutesText( timeToEndGame - Time.time, 20f ) );
	}



}
