using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour 
{
	public GameObject pickupPrefab;

	public PickupSpawnpoint[] pickupSpawnPoints;

	float timeForNextSpawn = -1f;

	// Use this for initialization
	public void StartSpawning()
	{
		SpawnPickupsAndSetNewTime();
	}

	/// <summary>
	/// Spawns the pickup.
	/// </summary>
	/// <returns>The pickup.</returns>
	private float SpawnPickup()
	{
		float destroyTime = Random.Range( 10f, 15f );
		GetRandomPickupToSpawnOn().SpawnPickup( pickupPrefab, destroyTime );
		return destroyTime;
	}

	private PickupSpawnpoint GetRandomPickupToSpawnOn()
	{
		List<PickupSpawnpoint> pickupSpawnPointsWithoutPickups = new List<PickupSpawnpoint>();

		for( int i = 0; i < pickupSpawnPoints.Length; i++ )
		{
			if( !pickupSpawnPoints[i].HasPickup() )
				pickupSpawnPointsWithoutPickups.Add( pickupSpawnPoints[i] );
		}

		return pickupSpawnPointsWithoutPickups[Random.Range( 0, pickupSpawnPointsWithoutPickups.Count )];
	}

	// Update is called once per frame
	void Update ()
	{
		if( timeForNextSpawn < 0f )
			return;

		if( timeForNextSpawn > Time.time )
			return;

		SpawnPickupsAndSetNewTime();
	}

	private void SpawnPickupsAndSetNewTime()
	{
		float longestDestroyTime = 0f;
		longestDestroyTime = SpawnPickup();
		float destroyTime = SpawnPickup();
		if( destroyTime > longestDestroyTime )
			longestDestroyTime = destroyTime;

		timeForNextSpawn = Time.time + longestDestroyTime;
	}
}
