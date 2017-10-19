using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public TMP_Text textMesh;

	float time;

	bool blinking;

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

		if( time <= 0f )
			Destroy( this.gameObject );
	}



	void StartBlinking()
	{
		// implement later
	}
}
