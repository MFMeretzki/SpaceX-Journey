using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravityTest : MonoBehaviour {

	public CosmicBody asteroid;
	public float gravity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = transform.position - asteroid.transform.position;
		direction.Normalize();
		asteroid.AddForce(direction * gravity);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
	}
}
