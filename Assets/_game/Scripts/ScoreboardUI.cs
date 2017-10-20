using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardUI : MonoBehaviour {

	public TMP_Text text;

	// Use this for initialization
	public void ShowScore( SessionData sessionData )
	{
		gameObject.SetActive( true );
		StartCoroutine( ShowScoreAnimation( sessionData ) );
	}

	IEnumerator ShowScoreAnimation( SessionData sessionData )
	{
		text.SetText( sessionData.pickupsCollected.ToString() );
		yield return new WaitForSeconds( 10f );
		SceneManager.LoadScene( SceneManager.GetActiveScene().name, LoadSceneMode.Single );
	}
}
