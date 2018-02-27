
using System.Collections;
using UnityEngine;

public class Spaceship : CosmicBody
{
	
    private const float ANGLE_TOLERANCE = 30.0f;

	public InputController inputC;
	public float thrustersForce;
	public float delayUntilMaxThrust;
	public float maxVelocity;
	public float fuelConsumption;
	public float maxLandingVelocity;
	public AudioClip thrustersAClip;
	public AudioSource thrustersAS;

	private GameController gameController;
	private BoxCollider2D collider2d;
	private Animator animator;
	private Vector2 direction;
	private float thrust;
	private bool thrustersActive = false;
	private bool landed = false;

	protected override void Start ()
	{
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
		gameController.GamePause += OnGamePause;
		collider2d = GetComponent<BoxCollider2D>();
		animator = GetComponentInChildren<Animator>();

		base.Start();
		direction = Vector2.up;
		thrust = 0;
		thrustersAS.volume = 0;
		thrustersAS.clip = thrustersAClip;
		thrustersAS.loop = true;
	}
	
	protected override void Update ()
	{
		base.Update();
		
        if (!GameController.Paused)
        {
			if (!landed && inputC.ChangeDirection())
            {
				direction = inputC.GetDirection();
                transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }

            if (thrustersActive && !inputC.ThrustersBurning())
            {
                animator.SetFloat("thrustersActive", 0);
                thrustersActive = false;
			}
            else if (!thrustersActive && inputC.ThrustersBurning())
            {
                animator.SetBool("thrustersBurning", true);
                animator.SetFloat("thrustersActive", 10);
                thrustersActive = true;
            }
            else if (thrustersActive == false && thrust == 0)
            {
                animator.SetBool("thrustersBurning", false);
            }
			float t = thrust / thrustersForce;
            animator.SetFloat("thrustersPotency", t);
			thrustersAS.volume = t * OptionsManager.Instance.GetEffectsVolume();

			if (t > 0 && !thrustersAS.isPlaying)
			{
				thrustersAS.Play();	
			}
			else if (t <= 0)
			{
				thrustersAS.Stop();
				thrustersAS.volume = 0;
			}
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
			thrust -= (Time.fixedDeltaTime / delayUntilMaxThrust) * thrustersForce;
			if (thrust < 0) thrust = 0;
        }

        if (rigidbody2d.velocity.magnitude > maxVelocity)
		{
			rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxVelocity;
		}
	}

    public bool IsLanded ()
    {
		return landed;
    }

	public void Respawn ()
	{
		transform.position = Vector3.zero;
		rigidbody2d.velocity = Vector2.zero;
	}


	protected override void OnCollisionEnter2D (Collision2D collision)
	{
		Vector2 dir = (collision.transform.position - transform.position).normalized;
		float dot = Vector2.Dot(direction, dir);
		if (dot <= -0.7)
		{
			if (collision.relativeVelocity.magnitude > maxLandingVelocity) GameOver();
		}
		else if (collision.relativeVelocity.magnitude > maxCollisionVelocity)
		{
			GameOver();
		}
	}

	protected void OnCollisionStay2D (Collision2D collision)
	{
		if (gameController.Planet != null)
		{
			Vector2 shipPlanetDir = (
				gameController.Planet.transform.position - transform.position
				);
			float angle = Vector2.Angle(shipPlanetDir.normalized, -direction);
			float radius = gameController.Planet.planetProperties.planetRadius;
			if (angle > ANGLE_TOLERANCE)
			{
				landed = false;
				GameOver();
			}
			
			else if (angle <= ANGLE_TOLERANCE &&
				shipPlanetDir.magnitude <= radius + (collider2d.size.y/2.0f) + 0.1f &&
				rigidbody2d.velocity.magnitude < 0.0001)
			{
				landed = true;
			}
			else landed = false;
		}
	}

	protected void OnCollissionExit2D (Collision2D collision)
	{
		landed = false;
	}

	protected void OnGamePause (bool paused)
	{
		if (paused)
		{
			thrustersAS.Pause();
		}
		else
		{
			thrustersAS.UnPause();
		}
	}


	private void GameOver ()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		gameController.ShipDestroied();
	}

}
