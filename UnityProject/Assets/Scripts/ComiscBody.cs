using UnityEngine;

public class ComiscBody : MonoBehaviour {

	private Rigidbody2D rigidbody2d;
	private Vector2 externalForces;
	
	void Start ()
	{
	}
	
	void Update ()
	{
		rigidbody2d.AddForce(externalForces);
		externalForces = Vector2.zero;
	}

	public void AddForce (Vector2 force)
	{
		externalForces += force;
	}
}
