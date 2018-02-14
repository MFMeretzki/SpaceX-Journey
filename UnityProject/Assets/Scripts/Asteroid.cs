using UnityEngine;

public class Asteroid : CosmicBody
{
	[SerializeField]
	private GameObject explosionPrefab;

	protected override void OnCollisionEnter2D (Collision2D collision)
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
