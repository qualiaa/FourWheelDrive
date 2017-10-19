using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SessionData
{
	public int pickupsCollected;
	public float gameEndTime;
}

public class GameManager : MonoBehaviour {

	public TMP_Text gameTimer;
	PickupManager pickupManager;

	SessionData sessionData;

	// Use this for initialization
	void Start ()
	{
		pickupManager = GetComponent<PickupManager>();
		StartGame();
	}

	private void StartGame()
	{
		sessionData = new SessionData
		{
			gameEndTime = Time.time + 60f
		};

		pickupManager.StartSpawning();
	}

	// Update is called once per frame
	void Update () 
	{
		if( sessionData == null)
			return;
		
		gameTimer.SetText( SplashJam.Utils.GetSecondsAndMinutesText( sessionData.gameEndTime - Time.time, 20f ) );
	}



}
