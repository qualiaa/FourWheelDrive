using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class SessionData
{
	public int pickupsCollected;
	public float gameEndTime;
	public bool isPlaying;
	public bool firstPickupPickedUp;
}

public class GameManager : MonoBehaviour {
	
	public float gameTime = 60f;
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
	public TMP_Text gameScoreText;

	public ScoreboardUI scoreboardUI;

	PickupManager pickupManager;
	SessionData sessionData;

	public delegate void OnGameEnd();
	public static event OnGameEnd OnGameEndMethods;

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
			gameEndTime = Time.time + gameTime,
			isPlaying = true
		};
		gameScoreText.SetText( "Pickups: " + sessionData.pickupsCollected.ToString() );

		pickupManager.SpawnFirstPickup();
	}

	// Update is called once per frame
	void Update ()
	{
		InGameUpdate();
	}

	private void InGameUpdate()
	{
		if( !IsPlaying() )
			return;

		gameTimer.SetText( SplashJam.Utils.GetSecondsAndMinutesText( sessionData.gameEndTime - Time.time, 20f ) );

		if( sessionData.gameEndTime <= Time.time)
			EndGame();
	}

	void EndGame()
	{
		sessionData.isPlaying = false;
		gameUIElement.SetActive( false );
		scoreboardUI.ShowScore( sessionData );
		if( OnGameEndMethods != null)
			OnGameEndMethods();
	}

	public void IncreaseScore()
	{
		if( !sessionData.firstPickupPickedUp )
		{
			sessionData.firstPickupPickedUp = true;
			pickupManager.StartContinuousSpawning();
		}
		sessionData.pickupsCollected++;
		gameScoreText.SetText( "Pickups: " + sessionData.pickupsCollected.ToString() );
		gameScoreText.transform.DOPunchScale( Vector3.one * 0.2f, 0.5f );
	}

	public bool IsPlaying()
	{
		if( sessionData == null )
			return false;

		if( !sessionData.isPlaying )
			return false;

		return true;
	}
}
