using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    const string RESTART_ID = "gameover_restart";
    const string MAIN_MENU_ID = "gameover_main_menu";
    const string OUT_OF_FUEL_ID = "gameover_out_of_fuel";
    const string SHIP_DESTROYED_ID = "gameover_ship_destroyed";

    public enum GameOver : int
    {
        OutOfFuel,
        ShipDestroid
    }

    [SerializeField]
    Text message;
    [SerializeField]
    Text score;
    [SerializeField]
    Text restart;
    [SerializeField]
    Text mainMenu;
    [SerializeField]
    Image sticker;
    [SerializeField]
    GameObject inputField;
    [SerializeField]
    Text inputText;

    bool isNewRecord;
    int points;

    private void OnEnable ()
    {
        restart.text = Localization.Instance.GetText(RESTART_ID);
        mainMenu.text = Localization.Instance.GetText(MAIN_MENU_ID);
        sticker.enabled = false;
        inputField.SetActive(false);
        isNewRecord = false;
    }

    void Start () { }
	void Update () { }

    public void  Show (GameOver gameOverCause, int ore)
    {
        gameObject.SetActive(true);

        message.text = Message(gameOverCause);
        points = ore;
        score.text = points.ToString();

        if (GameSettings.Instance.IsNewRecord(points) != -1)
        {
            sticker.enabled = true;
            inputField.SetActive(true);
            isNewRecord = true;
        }
    }

    public void RestartButtonClick ()
    {
        SaveRecord();
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButtonClick ()
    {
        SaveRecord();
        SceneManager.LoadScene("MainMenu");
    }

    private string Message (GameOver gameOverCause)
    {
        string msg = "";

        switch (gameOverCause)
        {
            case GameOver.OutOfFuel:
                msg = Localization.Instance.GetText(OUT_OF_FUEL_ID);
                break;
            case GameOver.ShipDestroid:
                msg = Localization.Instance.GetText(SHIP_DESTROYED_ID);
                break;
        }

        return msg;
    }

    private void SaveRecord ()
    {
        if (isNewRecord)
        {
            GameSettings.Instance.AddRecord(inputText.text, points);
        }
    }
}
