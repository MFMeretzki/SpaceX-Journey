using UnityEngine;

public class CosmicBody : MonoBehaviour {

	public Vector2 initialVelocity;
	public float initialAngularVelocity;
	public float maxCollisionVelocity;

	protected Rigidbody2D rigidbody2d;
	protected Vector2 externalForces;
	
	protected virtual void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.velocity = initialVelocity;
		rigidbody2d.angularVelocity = initialAngularVelocity;
	}
	
	protected virtual void Update ()
	{
		rigidbody2d.AddForce(externalForces);
		externalForces = Vector2.zero;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.relativeVelocity.magnitude > maxCollisionVelocity)
		{
			Debug.Log("high velocity collision");
		}
	}

	public void AddForce (Vector2 force)
	{
		externalForces += force;
	}
}
