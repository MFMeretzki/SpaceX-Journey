
using UnityEngine;

public class AsteroidFactory : MonoBehaviour {

	[SerializeField]
	private GameObject asteroidPrefab;
	[SerializeField]
	private GameObject asteroidsParent;
	[SerializeField]
	private Vector2 initialSpeedThreshold;
	[SerializeField]
	private float maxInitialAngularVel;

	private float radius;

	void Start ()
	{
		radius = asteroidPrefab.GetComponent<CircleCollider2D>().radius;
	}

	public GameObject BuildAsteroid (Vector3 spawnPosition, Vector2 direction)
	{
		Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f));
		GameObject obj = Instantiate(asteroidPrefab, spawnPosition, rotation, asteroidsParent.transform);

		CosmicBody cBody = obj.GetComponent<CosmicBody>();
		cBody.initialVelocity = direction *	Random.Range(initialSpeedThreshold.x, initialSpeedThreshold.y);
		cBody.initialAngularVelocity = Random.Range(0, maxInitialAngularVel);

		return obj;
	}

	public float GetAsteroidRadius ()
	{
		return radius;
	}
}
