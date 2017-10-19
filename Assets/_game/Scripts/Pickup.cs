using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Pickup : MonoBehaviour {

	public TMP_Text textMesh;

	float time;

	bool blinking;

	const float secondsLeftForBlink = 5f;

	// Use this for initialization
	public void SetTime( float time ) 
	{
		this.time = time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		time -= Time.deltaTime;
		float decialTextSize = 5f;

		string secondsText = SplashJam.Utils.GetSecondsAndMinutesText( time, decialTextSize );
		textMesh.SetText( secondsText );

		if( !blinking && time <= secondsLeftForBlink )
		{
			StartBlinking();
		}

		if( time <= 0f )
			Destroy( this.gameObject );
	}

	Color textColor0 = Color.white;
	Color textColor1 = Color.red;

	float textAlpha;

	void StartBlinking()
	{
		blinking = true;

		DOTween.To( () => textAlpha, x => textAlpha = x, 1f, 0.5f ).SetLoops( -1 ).OnUpdate( SetTextColor );
	}

	void SetTextColor()
	{
		textMesh.color = textAlpha > .5f ? textColor0 : textColor1;
	}
}
