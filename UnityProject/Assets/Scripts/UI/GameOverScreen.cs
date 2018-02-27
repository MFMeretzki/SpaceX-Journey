using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    const string RESTART_ID = "gameover_restart";
    const string MAIN_MENU_ID = "gameover_main_menu";
    const string PLACEHOLDER_ID = "gameover_name_placeholder";
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
    [SerializeField]
    Text placeholderText;

    bool isNewRecord = true;
    int points;

    private void OnEnable ()
    {
        restart.text = Localization.Instance.GetText(RESTART_ID);
        mainMenu.text = Localization.Instance.GetText(MAIN_MENU_ID);
        placeholderText.text = string.Format("{0}...", Localization.Instance.GetText(PLACEHOLDER_ID));
    }

    void Start () { }
	void Update () { }

    public void  Show (GameOver gameOverCause, int ore)
    {
        gameObject.SetActive(true);

        message.text = Message(gameOverCause);
        points = ore;
        score.text = points.ToString();

        if (GameSettings.Instance.IsNewRecord(points) == -1)
        {
            sticker.enabled = false;
            inputField.SetActive(false);
            isNewRecord = false;
        }
    }

    public void RestartButtonClick ()
    {
        SaveRecord();
        SceneManager.LoadScene(1);
    }

    public void MainMenuButtonClick ()
    {
        SaveRecord();
        SceneManager.LoadScene(0);
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
            string txt = 
                (inputText.text.Length != 0) ? inputText.text : Localization.Instance.GetText(PLACEHOLDER_ID);
            GameSettings.Instance.AddRecord(txt, points);
        }
    }
}
