using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Wheel : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character

	public float maxRotation = 45;
    [HideInInspector] public float power = 0;
	float angle = 0;
    float powerMultiplier = 10000;

    public Vector3 direction
    {
        get {
            return new Vector3(Mathf.Sin(angle * Mathf.PI / 180), 0, Mathf.Cos(angle * Mathf.PI / 180));
        }
    }
	public Vector3 totalForce
	{
		get {
            return direction * power;
		}
	}

	public float steerAmount
	{
		get { return angle; } //  angle / (1 + velocity) ; }
	}	

	[HideInInspector]
	public Player player; // The Rewired Player
	private float steerInput;
	private float gasInput;

	void Awake() 
	{
		if( DevTools.instance.twoControllersToRuleThemAll )
		{
			if( playerId == 0 || playerId == 1)
				player = ReInput.players.GetPlayer( 0 );
			else
				player = ReInput.players.GetPlayer( 1 );
			return;
		}
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}

	void Update () {
		GetInput();
		ProcessInput();
		var euler = Quaternion.Euler (new Vector3 (0,angle, 0));
		transform.localRotation = euler;
	}

	private void GetInput() {
		
		// Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
		// whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

		steerInput = player.GetAxis( RewiredConsts.Action.Steer ); // get input by name or action id

		if( !GameManager.gm.IsPlaying())
			return;
		
		gasInput = player.GetAxis( RewiredConsts.Action.Gas ); // get input by name or action id
	}

	private void ProcessInput() {
		// Process movement
		if (Mathf.Abs (steerInput) > 0.1f)
		{
			angle = steerInput * maxRotation;//transform.Translate( new Vector3( steerInput, 0f, gasInput ) * moveSpeed * Time.deltaTime );
		}
		else
		{
            angle = 0;
		}
        if (Mathf.Abs(gasInput) > 0.1f)
        {
            power = powerMultiplier*gasInput;
        }
        else
        {
            power = 0;
        }
	}
}
