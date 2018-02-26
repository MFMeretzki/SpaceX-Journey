using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {

    static bool gameOver;
    public static bool GameOver { get { return gameOver; } }
    static bool paused;
    public static bool Paused { get { return paused; } }
    public static void Pause (bool paused)
    {
        GameController.paused = paused;
        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    [SerializeField]
    private Transform ship;
    public Transform Ship
    {
        get { return ship; }
    }
    public Planet Planet { get; set; }
    [SerializeField]
    private MapManager mapManager;
    public MapManager MapManager { get { return mapManager; } }

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameOverScreen gameOverScreen;

    [SerializeField]
    private float fuelCapacity;
    public float FuelCapacity { get{return fuelCapacity;} }
    private float fuel;
    private int ore;


    void Awake ()
    {
        gameOver = false;
        paused = false;
        Planet = null;
        fuel = fuelCapacity;
        ore = 0;
	}

	void Start () { }
	void Update () { }

	void OnDisable ()
    {
        GameController.Pause(false);
    }

    public void FuelConsumption(float volumeConsumed)
    {
        fuel -= volumeConsumed;
        if (FuelChange != null) FuelChange(fuel);
        if (fuel <= 0.0f) OutOfFuel();
    }

    public void FuelCollected (float fuelAmount)
    {
        fuel += fuelAmount;
        if (fuel > fuelCapacity) fuel = fuelCapacity;
        if (FuelChange != null) FuelChange(fuel);
    }

    public void OreCollected(int oreAmount)
    {
        ore += oreAmount;
        if (OreChange != null) OreChange(ore);
    }

	public void ShipDestroied ()
	{
		ship.gameObject.SetActive(false);
        if (!gameOver)
		    StartCoroutine(GameOverCoroutine(GameOverScreen.GameOver.ShipDestroid, 2f));
	}

    private void OutOfFuel ()
    {
        if (!gameOver)
            StartCoroutine(GameOverCoroutine(GameOverScreen.GameOver.OutOfFuel, 2f));
    }

	private IEnumerator GameOverCoroutine (GameOverScreen.GameOver gameOverCause, float seconds)
	{
        GameController.gameOver = true;
		yield return new WaitForSeconds(seconds);
        gameOverScreen.Show(gameOverCause, ore);
	}

    public void MenuButtonPressed ()
    {
        bool p = !GameController.Paused;
        GameController.Pause(p);
        pauseMenu.SetActive(p);
		if (GamePause != null) GamePause(p);
    }

    #region Events
    public delegate void FuelChangeDelegate (float fuel);
	public event FuelChangeDelegate FuelChange;

    public delegate void OreChangeDelegate (int fuel);
	public event OreChangeDelegate OreChange;

	public delegate void GamePauseDelegate (bool paused);
	public event GamePauseDelegate GamePause;
    #endregion
}
