using UnityEngine;

public class Spaceship : CosmicBody {

    private GameController gameController;

	public InputController inputC;
	public float thrustersForce;
	public float delayUntilMaxThrust;
	public float maxVelocity;
	public float fuelConsumption; 

	private Vector2 direction;
	private float thrust;

	protected override void Start ()
	{
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
		base.Start();
		direction = Vector2.up;
		thrust = 0;
	}
	
	protected override void Update ()
	{
		base.Update();

		if (inputC.ChangeDirection())
		{
			direction = inputC.GetDirection();
			transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
		}
	}

	void FixedUpdate ()
    {
        if (inputC.ThrustersBurning())
        {
            thrust += (Time.fixedDeltaTime / delayUntilMaxThrust) * thrustersForce;
            if (thrust > thrustersForce) thrust = thrustersForce;
            rigidbody2d.AddForce(direction * thrust);
            gameController.FuelConsumption(fuelConsumption * Time.fixedDeltaTime);
        }
        else
        {
            thrust = 0;
        }

        if (rigidbody2d.velocity.magnitude > maxVelocity)
		{
			rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxVelocity;
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
	}
}
