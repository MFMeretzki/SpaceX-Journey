using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : BasePanel {

	private const string LANGUAGE_ID = "options_menu_language";
	private const string MUSIC_VOLUME_ID = "options_menu_music_volume";
	private const string EFFECTS_VOLUME_ID = "options_menu_effects_volume";
	private const string RETURN_ID = "options_menu_return";
	
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
	private Slider effectsVolumeSlider;

	void Start ()
	{
		OnLanguageChange();
		musicVolumeSlider.value = OptionsManager.Instance.GetMusicVolume();
		effectsVolumeSlider.value = OptionsManager.Instance.GetEffectsVolume();
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

	public void ReturnButtonClick ()
	{
		menuController.SwitchPanel(0);
	}


	protected override void OnLanguageChange ()
	{
		languageText.text = Localization.Instance.GetText(LANGUAGE_ID);
		musicVolumeText.text = Localization.Instance.GetText(MUSIC_VOLUME_ID);
		effectsVolumeText.text = Localization.Instance.GetText(EFFECTS_VOLUME_ID);
		returnText.text = Localization.Instance.GetText(RETURN_ID);
	}
}
