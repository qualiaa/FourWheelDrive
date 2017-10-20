using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class MenuUI : MonoBehaviour 
{
	public Image[] icons;

	Image[] fillbars;

	// Use this for initialization
	void Start () 
	{
		fillbars = new Image[icons.Length];
		for( int i = 0; i < fillbars.Length; i++ )
		{
			fillbars[i] = icons[i].transform.GetChild( 0 ).GetComponent<Image>();
		}
	}

	float combinedAcceleration;

	// Update is called once per frame
	void Update () 
	{
		combinedAcceleration = 0f;
		for( int i = 0; i < icons.Length; i++ )
		{
			Player player = ReInput.players.GetPlayer( i );

			float gasInput = player.GetAxis( RewiredConsts.Action.Gas );
			combinedAcceleration += gasInput;
			fillbars[i].fillAmount = gasInput;
			icons[i].transform.localRotation =
				Quaternion.Euler( 0f, 0f, player.GetAxis( RewiredConsts.Action.Steer ) * -30f );
		}
		if( combinedAcceleration > 3.7f )
		{
			CountDownUI.instance.StartCountDown();
			gameObject.SetActive( false );
		}
	}




}
