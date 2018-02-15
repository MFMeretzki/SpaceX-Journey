using UnityEngine;

public class OptionsManager
{

	private const string LANGUAGE = "LANGUAGE";
	private const string EFFECTS_VOLUME = "EFFECTS_VOLUME";
	private const string MUSIC_VOLUME = "MUSIC_VOLUME";

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


	public delegate void LanguageChange ();
	public event LanguageChange OnLanguageChange;

	/// <summary>
	/// Constructor. If it doesn't find the player prefs uses default values.
	/// </summary>

	private OptionsManager ()
	{
		language = PlayerPrefs.GetString(LANGUAGE, "English");
		effectsVolume = PlayerPrefs.GetFloat(EFFECTS_VOLUME, 0.5f);
		musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.5f);
	}

	public string GetLanguage ()
	{
		return language;
	}

	public float GeteffectsVolume ()
	{
		return effectsVolume;
	}

	public float GetmusicVolume ()
	{
		return musicVolume;
	}

	public void SetLanguage (string newLanguage)
	{
		PlayerPrefs.SetString(LANGUAGE, newLanguage);
		language = newLanguage;
		Localization.Instance.SetLanguage(newLanguage);
		ChangeLanguage();
	}

	public void SetEffectsVolume (float newEffectsVolume)
	{
		PlayerPrefs.SetFloat(EFFECTS_VOLUME, newEffectsVolume);
		effectsVolume = newEffectsVolume;
		SoundManager.Instance.SetEffectVolume(newEffectsVolume);
	}

	public void SetMusicVolume (float newMusicVolume)
	{
		PlayerPrefs.SetFloat(MUSIC_VOLUME, newMusicVolume);
		musicVolume = newMusicVolume;
		SoundManager.Instance.SetMusicVolume(newMusicVolume);

	}

	private void ChangeLanguage ()
	{

		if (OnLanguageChange != null)
		{
			OnLanguageChange();
		}
	}

}
