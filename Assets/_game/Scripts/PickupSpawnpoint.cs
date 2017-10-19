using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnpoint : MonoBehaviour {

	Pickup spawnedPickup;

	public void SpawnPickup( GameObject pickupPrefab, float time )
	{
		if( HasPickup() )
			return;

		spawnedPickup = Instantiate( pickupPrefab, transform.position, transform.rotation )
			.GetComponent<Pickup>();

		spawnedPickup.SetTime( time );
	}

	public bool HasPickup()
	{
		return spawnedPickup != null;
	}

}
