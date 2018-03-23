using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

	[SerializeField]
	private GameController gameController;
	[SerializeField]
	private AsteroidFactory asteroidFactory;
	[SerializeField]
	private float minTimeBetweenSpawn;
	[SerializeField]
	private float minSpawnDistance;
	[SerializeField]
	private float maxDistanceToPlayer;

	private List<GameObject> asteroids;
	private float nextSpawn = 0;
	private Collider2D[] collArray = new Collider2D[1];

	void Start ()
	{
		asteroids = new List<GameObject>();
	}

	void Update ()
	{
		if (Time.time > nextSpawn)
		{
			SpawnAsteroid();
		}

		if (Time.frameCount % 10 == 0)
		{
			RemoveAsteroids();
		}
	}


	private void SpawnAsteroid ()
	{
		Vector2 pos = gameController.Ship.transform.position;
        Vector2 vel = gameController.Ship.Velocity * 7.5f;
        Vector2 spawnPos = pos + vel + Random.insideUnitCircle.normalized * minSpawnDistance;
		float radius = asteroidFactory.GetAsteroidRadius();
		while (Physics2D.OverlapCircleNonAlloc(spawnPos, radius, collArray) != 0)
		{
            spawnPos = pos + vel + Random.insideUnitCircle.normalized * minSpawnDistance;
            //spawnPos = pos + Random.insideUnitCircle.normalized * minSpawnDistance;
        }
		
		Vector2 direction;
        if (gameController.Planet != null)
        {
            direction = Random.insideUnitCircle;
			//TODO what to do when spaceship is near a planet?
			//no spawn asteroid for now
			return;
        }
        else
        {
            direction = pos + vel - spawnPos;
        }
		GameObject asteroid = asteroidFactory.BuildAsteroid(spawnPos, direction.normalized);
		asteroids.Add(asteroid);

		nextSpawn = Time.time + minTimeBetweenSpawn + Random.Range(0, 4);
	}

	private void RemoveAsteroids ()
	{
		Vector3 spaceshipPos = gameController.Ship.transform.position;
		for (int i = asteroids.Count - 1; i >= 0; --i)
		{
			GameObject obj = asteroids[i];
			if (obj == null) asteroids.RemoveAt(i);
			else
			{
				float distance = (obj.transform.position - spaceshipPos).magnitude;
				if (distance > maxDistanceToPlayer)
				{
					asteroids.RemoveAt(i);
					Destroy(obj);
				}
			}
		}
	}

}
