using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerLight : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player

	Light lightComp;

	void Awake()
	{
		if( DevTools.instance.twoControllersToRuleThemAll )
		{
			if( playerId == 0 || playerId == 1 )
				player = ReInput.players.GetPlayer( 0 );
			else
				player = ReInput.players.GetPlayer( 1 );
			return;
		}
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer( playerId );
	}

	private void Start()
	{
		lightComp = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update () 
	{
		GetInput();
	}

	private void GetInput()
	{
		// Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
		// whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

		if( player.GetButtonDown(RewiredConsts.Action.LightSwitch) )
		{
			lightComp.enabled = !lightComp.enabled;
		}
	}
}
