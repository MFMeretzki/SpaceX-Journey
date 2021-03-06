using UnityEngine;

public class OptionsManager
{

	private const string LANGUAGE = "LANGUAGE";
	private const string EFFECTS_VOLUME = "EFFECTS_VOLUME";
	private const string MUSIC_VOLUME = "MUSIC_VOLUME";
    private const string CONTROLS = "CONTROLS";

	private static OptionsManager instance;
	public static OptionsManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new OptionsManager();
			}
			return instance;
		}
	}

	private string language;
	private float effectsVolume;
	private float musicVolume;
    private string controls;


    /// <summary>
    /// Constructor. If it doesn't find the player prefs uses default values.
    /// </summary>

    private OptionsManager ()
	{
		language = PlayerPrefs.GetString(LANGUAGE, "English");
		effectsVolume = PlayerPrefs.GetFloat(EFFECTS_VOLUME, 0.5f);
		musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.5f);
        controls = PlayerPrefs.GetString(CONTROLS, "TouchnGo");
    }

	public string GetLanguage ()
	{
		return language;
	}

	public float GetEffectsVolume ()
	{
		return effectsVolume;
	}

	public float GetMusicVolume ()
	{
		return musicVolume;
	}

    public string GetControls ()
    {
        return controls;
    }

	public void SetLanguage (string newLanguage)
	{
		language = newLanguage;
		PlayerPrefs.SetString(LANGUAGE, language);
		Localization.Instance.SetLanguage(language);
		OnLanguageChange();
	}

	public void SetMusicVolume (float newMusicVolume)
	{
		musicVolume = newMusicVolume;
		PlayerPrefs.SetFloat(MUSIC_VOLUME, musicVolume);
		SoundManager.Instance.SetMusicVolume(musicVolume);

	}

	public void SetEffectsVolume (float newEffectsVolume)
	{
		effectsVolume = newEffectsVolume;
		PlayerPrefs.SetFloat(EFFECTS_VOLUME, effectsVolume);
		SoundManager.Instance.SetEffectVolume(effectsVolume);
    }

    public void SetControl (string newControls)
    {
        controls = newControls;
        PlayerPrefs.SetString(CONTROLS, controls);
        OnControlsChange();
    }

    #region events
    public delegate void LanguageChangeHandler ();
	public event LanguageChangeHandler LanguageChange;

    private void OnLanguageChange ()
    {
        if (LanguageChange != null) { LanguageChange(); }
    }


    public delegate void ControlsChangeHandler ();
    public event LanguageChangeHandler ControlsChange;

    private void OnControlsChange ()
    {
        if (ControlsChange != null) { ControlsChange(); }
    }
    #endregion
}
