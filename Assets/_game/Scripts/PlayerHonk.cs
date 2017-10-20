using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerHonk : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character
    public AudioSource honk;
    [HideInInspector] Player player;

    void Awake () {
        player = ReInput.players.GetPlayer( playerId );
	}
	
	// Update is called once per frame
	void Update () {
        if (playerId != 1) // military player has loop
        {
            if (player.GetButtonDown(RewiredConsts.Action.Honk))
            {
                if (!honk.isPlaying)
                {
                    honk.Play();
                }
            }
        }
        else {
            if (player.GetButton (RewiredConsts.Action.Honk))
            {
                Debug.Log("Button down");
                if (!honk.isPlaying)
                {
                    Debug.Log("Honk wasn't playing");
                    honk.Play();
                }
            }
            else
            {
                Debug.Log("Button not down");
                if (honk.isPlaying)
                {
                    Debug.Log("Honk was playing");
                    honk.Stop();
                }
            }
        }
	}
}
