using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Wheel : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character

	public float moveSpeed = 3.0f;

	private Player player; // The Rewired Player
	private float steerInput;
	private float gasInput;

	void Awake() 
	{
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}

	void Update () {
		GetInput();
		ProcessInput();
	}

	private void GetInput() {
		// Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
		// whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

		steerInput = player.GetAxis( RewiredConsts.Action.Steer ); // get input by name or action id
		gasInput = player.GetAxis( RewiredConsts.Action.Gas ); // get input by name or action id
	}

	private void ProcessInput() {
		// Process movement
		if( ( Mathf.Abs( steerInput ) > 0.1f
			 || Mathf.Abs( gasInput ) > 0.1f ) )
		{
			transform.Translate( new Vector3( steerInput, 0f, gasInput ) * moveSpeed * Time.deltaTime );
		}
	}
}
