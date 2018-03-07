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
	public bool IngameSounds { private set; get; }

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

	void Start ()
	{
		SoundManager.Instance.PlayMusic(0, true, 2f);
		IngameSounds = true;
	}

	void Update () { }

	void OnDisable ()
    {
        GameController.Pause(false);
		SoundManager.Instance.StopMusic();
    }

	void OnApplicationFocus (bool hasFocus)
	{
		if (!gameOver && !Paused && !hasFocus) PauseGame(true);
	}

	void OnApplicationPause (bool pauseStatus)
	{
		if (!gameOver && !Paused && pauseStatus) PauseGame(true);
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
		{
			float seconds = 2f;
			SoundManager.Instance.MusicFadeOut(seconds);
			StartCoroutine(GameOverCoroutine(GameOverScreen.GameOver.ShipDestroid, seconds));
		}
	}

    private void OutOfFuel ()
    {
		if (!gameOver)
		{
			float seconds = 2f;
			SoundManager.Instance.MusicFadeOut(seconds);
			StartCoroutine(GameOverCoroutine(GameOverScreen.GameOver.OutOfFuel, seconds));
		}

    }

	private IEnumerator GameOverCoroutine (GameOverScreen.GameOver gameOverCause, float seconds)
	{
		GameController.gameOver = true;
		yield return new WaitForSeconds(seconds);
		IngameSounds = false;
		gameOverScreen.Show(gameOverCause, ore);
		if (GameOverEvent != null) GameOverEvent();
	}

	private void PauseGame (bool p)
	{
		GameController.Pause(p);
		pauseMenu.SetActive(p);
		SoundManager.Instance.PauseMusic(p);
		if (GamePause != null) GamePause(p);
	}

    public void MenuButtonPressed ()
    {
        bool p = !GameController.Paused;
		PauseGame(p);
    }

    #region Events
    public delegate void FuelChangeHandler (float fuel);
	public event FuelChangeHandler FuelChange;

    public delegate void OreChangeHandler (int fuel);
	public event OreChangeHandler OreChange;

	public delegate void GamePauseHandler (bool paused);
	public event GamePauseHandler GamePause;

	public delegate void GameOverHandler ();
	public event GameOverHandler GameOverEvent;
    #endregion
}
