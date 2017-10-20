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

	public static GameManager gm;
	private void Awake() { gm = this; }

	public enum GameState
	{
		none,
		menu,
		playing
	}

	public GameState currentState;

	public GameObject menuUIElement;

	public GameObject gameUIElement;
	public TMP_Text gameTimer;

	PickupManager pickupManager;
	SessionData sessionData;

	// Use this for initialization
	void Start ()
	{
		pickupManager = GetComponent<PickupManager>();
		switch( currentState )
		{
		case GameState.menu:
			ShowMenu();
			break;
		case GameState.playing:
			StartGame();
			break;
		default:
			break;
		}
	}

	private void ShowMenu()
	{
		menuUIElement.SetActive( true );
		gameUIElement.SetActive( false );
	}

	public void StartGame()
	{
		menuUIElement.SetActive( false );
		gameUIElement.SetActive( true );
		sessionData = new SessionData
		{
			gameEndTime = Time.time + 60f
		};

		pickupManager.StartSpawning();
	}

	// Update is called once per frame
	void Update ()
	{
		InGameUpdate();
	}

	private void InGameUpdate()
	{
		if( sessionData == null )
			return;

		gameTimer.SetText( SplashJam.Utils.GetSecondsAndMinutesText( sessionData.gameEndTime - Time.time, 20f ) );
	}
}
