using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class Pickup : MonoBehaviour {

	public TMP_Text textMesh;

	float time = -100f;

	bool blinking;

	const float secondsLeftForBlink = 5f;

	public ParticleSystem explostionParticle;

    AudioSource smashSound;

    public void Start()
    {
        smashSound = GameObject.Find("Pickup Sound Source").GetComponent<AudioSource>();
    }

	// Use this for initialization
	public void SetTime( float time ) 
	{
		if( time < -99f )
		{
			textMesh.gameObject.SetActive( false );
			return;
		}
		
		this.time = time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( time < -99f )
			return;
		
		time -= Time.deltaTime;
		float decialTextSize = 5f;

		string secondsText = SplashJam.Utils.GetSecondsAndMinutesText( time, decialTextSize );
		textMesh.SetText( secondsText );

		if( !blinking && time <= secondsLeftForBlink)
			StartBlinking();

		if( time <= 0f )
			Destroy( gameObject );
	}

	Color textColor0 = Color.white;
	Color textColor1 = Color.red;

	float textAlpha;

	void StartBlinking()
	{
		blinking = true;
		DOTween.To( () => textAlpha, x => textAlpha = x, 1f, 0.5f ).SetLoops( -1 ).OnUpdate( SetTextColor );
	}

	private void OnTriggerEnter( Collider other )
	{
		Instantiate( explostionParticle, transform.position, Quaternion.Euler( -90f, 0f, 0f ) );
        smashSound.Play();
		GameManager.gm.IncreaseScore();
		Destroy(gameObject);
	}

	void SetTextColor()
	{
		textMesh.color = textAlpha > .5f ? textColor0 : textColor1;
	}

	private void OnEnable()
	{
		GameManager.OnGameEndMethods += OnGameEnd;
	}

	private void OnDisable()
	{
		GameManager.OnGameEndMethods -= OnGameEnd;
	}
	
	private void OnGameEnd()
	{
		Destroy( gameObject );
	}

}
