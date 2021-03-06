using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : BasePanel
{
	private const string RESUME_ID = "pause_resume";
	private const string RESTART_ID = "pause_restart";
    private const string OPTIONS_ID = "pause_options";
    private const string MAIN_MENU_ID = "pause_main_menu";
    private const string EXIT_ID = "pause_exit";

	[SerializeField]
	private Text resumeText;
    [SerializeField]
    private Text restartText;
    [SerializeField]
    private Text optionsText;
    [SerializeField]
    private Text mainMenuText;
    [SerializeField]
    private Text exitText;


    protected override void Awake ()
    {
        OnLanguageChange();
    }

    public void RestartButtonClick ()
    {
        SceneManager.LoadScene("InterstitialAd");
    }

    public void OptionsButtonClick ()
    {
        menuController.SwitchPanel(1);
    }

    public void MainMenuButtonClick ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButtonClick ()
    {
        Application.Quit();
    }

    protected override void OnLanguageChange ()
    {
		resumeText.text = Localization.Instance.GetText(RESUME_ID);
        restartText.text = Localization.Instance.GetText(RESTART_ID);
        optionsText.text = Localization.Instance.GetText(OPTIONS_ID);
        mainMenuText.text = Localization.Instance.GetText(MAIN_MENU_ID);
        exitText.text = Localization.Instance.GetText(EXIT_ID);
    }
}
