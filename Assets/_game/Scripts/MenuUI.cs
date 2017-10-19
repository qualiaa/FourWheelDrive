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
	
	// Update is called once per frame
	void Update () 
	{
		for( int i = 0; i < icons.Length; i++ )
		{
			Player player = ReInput.players.GetPlayer( i );
			fillbars[i].fillAmount = player.GetAxis( RewiredConsts.Action.Gas );
		}
	}

	void SetPlayerJoined( int id )
	{
		icons[id].color = Color.red;
	}
}
