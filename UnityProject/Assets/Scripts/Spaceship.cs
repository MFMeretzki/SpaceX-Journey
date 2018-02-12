using UnityEngine;

public class Spaceship : CosmicBody {

	public InputController inputC;
	public float thrustersForce;
	public float delayUntilMaxThrust;
	public float maxVelocity;
	public float fuelTankCapacity;
	public float fuelConsumption; 

	private Vector2 direction;
	private float thrust;
	public float fuel;

	protected override void Start ()
	{
		base.Start();
		direction = Vector2.up;
		thrust = 0;
		fuel = fuelTankCapacity;
	}
	
	protected override void Update ()
	{
		base.Update();

		if (inputC.ChangeDirection())
		{
			direction = inputC.GetDirection();
			transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
		}
		if (inputC.ThrustersBurning())
		{
			thrust += (Time.deltaTime/ delayUntilMaxThrust) * thrustersForce;
			if (thrust > thrustersForce) thrust = thrustersForce;
			rigidbody2d.AddForce(direction * thrust);
			fuel -= fuelConsumption * Time.deltaTime;
		}
		else
		{
			thrust = 0;
		}
	}

	void FixedUpdate ()
	{
		if (rigidbody2d.velocity.magnitude > maxVelocity)
		{
			rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxVelocity;
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
	}
}
