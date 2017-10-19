using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	PickupManager pickupManager;

	// Use this for initialization
	void Start () 
	{
		pickupManager = GetComponent<PickupManager>();

		pickupManager.StartSpawning();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}



}
