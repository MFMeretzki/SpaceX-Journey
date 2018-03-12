using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

	private const string SPEED_ID = "ingame_speed";

	[SerializeField]
	private Image speedometerFill;
	[SerializeField]
	private Text speedometerText;

	private GameController gameController;
	private Rigidbody2D spaceshipRb;
	private float inverseMaxVelocity;

	
	void Start ()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		spaceshipRb = gameController.Ship.GetComponent<Rigidbody2D>();

		Spaceship spaceship = gameController.Ship.GetComponent<Spaceship>();
		inverseMaxVelocity = 1f / spaceship.maxVelocity;

		OnLanguageChange();
	}
	

	void Update ()
	{
		speedometerFill.fillAmount = spaceshipRb.velocity.magnitude * inverseMaxVelocity;
	}

	void OnEnable ()
	{
		OptionsManager.Instance.LanguageChange += OnLanguageChange;
	}

	void OnDisable ()
	{
		OptionsManager.Instance.LanguageChange -= OnLanguageChange;
	}


	private void OnLanguageChange ()
	{
		speedometerText.text = Localization.Instance.GetText(SPEED_ID);
	}
}
