using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : BasePanel {

	private const string LANGUAGE_ID = "options_menu_language";
	private const string MUSIC_VOLUME_ID = "options_menu_music_volume";
	private const string EFFECTS_VOLUME_ID = "options_menu_effects_volume";
    private const string CONTROLS_ID = "options_menu_controls";
    private const string JOYSTICK_ID = "options_menu_joystick";
    private const string TOUCHNGO_ID = "options_menu_touchngo";
    private const string RETURN_ID = "options_menu_return";

    private readonly Color COLOR_SELECTED = new Color(1.0f, 1.0f, 1.0f);
    private readonly Color COLOR_DESELECTED = new Color(0.6f, 0.6f, 0.6f);

    [SerializeField]
    private Button[] controls;

    [SerializeField]
	private Text languageText;
	[SerializeField]
	private Text musicVolumeText;
	[SerializeField]
	private Text effectsVolumeText;
	[SerializeField]
	private Text returnText;
	[SerializeField]
	private Slider musicVolumeSlider;
    [SerializeField]
    private Text controlsText;
    [SerializeField]
    private Text joystickText;
    [SerializeField]
    private Text touchngoText;
    [SerializeField]
	private Slider effectsVolumeSlider;
    
    private Dictionary<string, Button> controlsDic;

    protected override void Awake ()
    {
        base.Awake();
        controlsDic = new Dictionary<string, Button>();
        controlsDic.Add("Joystick", controls[0]);
        controlsDic.Add("TouchnGo", controls[1]);
    }

    protected override void OnEnable ()
    {
        base.OnEnable();
        OptionsManager.Instance.ControlsChange += OnControlsChange;
    }

    void Start ()
	{
		OnLanguageChange();
        OnControlsChange();

        musicVolumeSlider.value = OptionsManager.Instance.GetMusicVolume();
		effectsVolumeSlider.value = OptionsManager.Instance.GetEffectsVolume();
	}

    protected override void OnDisable ()
    {
        base.OnDisable();
        OptionsManager.Instance.ControlsChange -= OnControlsChange;
    }


	public void SetLanguage (string language)
	{
		OptionsManager.Instance.SetLanguage(language);
	}

	public void ChangeMusicVolume ()
	{
		OptionsManager.Instance.SetMusicVolume(musicVolumeSlider.value);
	}

	public void ChangeEffectsVolume ()
	{
		OptionsManager.Instance.SetEffectsVolume(effectsVolumeSlider.value);
    }

    public void SetControl (string controls)
    {
        OptionsManager.Instance.SetControl(controls);
    }

    public void ReturnButtonClick ()
	{
		menuController.SwitchPanel(0);
	}


	protected override void OnLanguageChange ()
	{
		languageText.text = Localization.Instance.GetText(LANGUAGE_ID);
		musicVolumeText.text = Localization.Instance.GetText(MUSIC_VOLUME_ID);
		effectsVolumeText.text = Localization.Instance.GetText(EFFECTS_VOLUME_ID);
        controlsText.text = Localization.Instance.GetText(CONTROLS_ID);
        joystickText.text = Localization.Instance.GetText(JOYSTICK_ID);
        touchngoText.text = Localization.Instance.GetText(TOUCHNGO_ID);
        returnText.text = Localization.Instance.GetText(RETURN_ID);
	}

    protected virtual void OnControlsChange ()
    {
        string c = OptionsManager.Instance.GetControls();
        foreach (var b in controlsDic)
        {
            ColorBlock colorBlock = b.Value.colors;
            if (b.Key == c)
            {
                colorBlock.normalColor = COLOR_SELECTED;
            }
            else
            {
                colorBlock.normalColor = COLOR_DESELECTED;
            }
            b.Value.colors = colorBlock;
        }
    }
}
