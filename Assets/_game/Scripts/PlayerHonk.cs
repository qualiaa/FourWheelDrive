using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerHonk : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character
    public AudioSource honk;
    [HideInInspector] Player player;
	public ParticleSystem ps;

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
					PlaySoundsAndParticle();
				}
			}
        }
        else {
            if (player.GetButton (RewiredConsts.Action.Honk))
            {
                if (!honk.isPlaying)
                {
                    PlaySoundsAndParticle();
                }
            }
            else
            {
                if (honk.isPlaying)
                {
                    honk.Stop();
                }
            }
        }
	}

	[ContextMenu("PlaySound")]
	private void PlaySoundsAndParticle()
	{
		honk.Play();

	}

}
