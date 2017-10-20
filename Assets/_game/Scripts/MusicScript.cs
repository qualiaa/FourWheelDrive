using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    public AudioSource music;
    public AudioClip musicLoop;

	// Use this for initialization
    void Start () {
        music.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!music.isPlaying)
        {
            music.clip = musicLoop;
            music.loop = true;
            music.Play();
        }
	}
}
