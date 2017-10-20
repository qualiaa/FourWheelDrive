using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CountDownUI : MonoBehaviour {

	public static CountDownUI instance;
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		gameObject.SetActive( false );
	}

	public TMP_Text text;

	// Use this for initialization
	public void StartCountDown() 
	{
		gameObject.SetActive( true );
		StartCoroutine( CountDownAnimation() );
	}

	IEnumerator CountDownAnimation()
	{
		for( int i = 3; i >= 1; i-- )
		{
			text.SetText( i.ToString() );
			text.transform.localScale = Vector3.zero;
			CamShaker.instance.ShakeCountdown();
			yield return text.transform.DOScale( Vector3.one * 3, 1f ).WaitForCompletion();
		}
		GameManager.gm.StartGame();
		text.SetText( "GO!" );
		CamShaker.instance.ShakeCountdown();
		text.transform.localScale = Vector3.zero;
		yield return text.transform.DOScale( Vector3.one * 3, 1f ).WaitForCompletion();
		gameObject.SetActive( false );
	}
}
