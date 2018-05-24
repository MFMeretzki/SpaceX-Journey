using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : BasePanel
{

    private const string TITLE_ID = "main_menu_info";
    private const string INFO_ID = "info_text";
    private const string RETURN_ID = "options_menu_return";

    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text infoText;
    [SerializeField]
    private Text returnText;

    void Start ()
    {
        OnLanguageChange();
    }


    public void ReturnButtonClick ()
    {
        menuController.SwitchPanel(0);
    }


    protected override void OnLanguageChange ()
    {
        titleText.text = Localization.Instance.GetText(TITLE_ID);
        infoText.text = Localization.Instance.GetText(INFO_ID);
        returnText.text = Localization.Instance.GetText(RETURN_ID);
    }
}
