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

		float timeDecimals = ( time - Mathf.Floor( time ) ) * 100f;

		textMesh.SetText( string.Format( 
		                                "{0}<size=5>.{1}</size>", 
		                                time.ToString( "0" ), 
		                                timeDecimals.ToString( "00" ).Substring( 0, 2 ) ) );

		if( time <= 0f)
			Destroy( this.gameObject );
	}

	void StartBlinking()
	{
		// implement later
	}
}
