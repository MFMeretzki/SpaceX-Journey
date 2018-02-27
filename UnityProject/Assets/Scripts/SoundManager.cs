using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	[SerializeField]
	private AudioSource musicASource;
	[SerializeField]
	private AudioSource[] effectsASource;
	[SerializeField]
	private AudioClip[] musicClips;

	private Queue<AudioSource> effectsASourceQueue;
	private bool musicFadeIn = false;
	private bool musicFadeOut = false;
	private float fadeStep;

	private static SoundManager instance;
	public static SoundManager Instance
	{
		get
		{
			return instance;
		}
	}

	void Awake ()
	{

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		musicASource.volume = OptionsManager.Instance.GetMusicVolume();

		effectsASourceQueue = new Queue<AudioSource>();
		float eVolume = OptionsManager.Instance.GetEffectsVolume();
		for (int i = 0; i < effectsASource.Length; ++i)
		{
			effectsASource[i].volume = eVolume;
			effectsASourceQueue.Enqueue(effectsASource[i]);
		}
	}

	void FixedUpdate ()
	{
		if (musicFadeIn)
		{
			musicASource.volume += fadeStep;
			if (musicASource.volume >= OptionsManager.Instance.GetMusicVolume())
			{
				musicASource.volume = OptionsManager.Instance.GetMusicVolume();
				musicFadeIn = false;
			}
		}
		if (musicFadeOut)
		{
			musicASource.volume += fadeStep;
			if (musicASource.volume <= 0) musicFadeOut = false;
		}
	}



	public void SetMusicVolume (float musicVolume)
	{
		musicASource.volume = musicVolume;
	}

	public void SetEffectVolume (float effectsVolume)
	{
		for (int i = 0; i < effectsASource.Length; ++i)
		{
			effectsASource[i].volume = effectsVolume;
		}
	}


	/// <summary>
	/// Plays a sound effect, if all audiosources are being used prints a message
	/// </summary>
	/// <param name="clip">clip containing the effect to play</param>
	public void PlayEffect (AudioClip clip)
	{
		if (effectsASourceQueue.Count > 0)
		{
			AudioSource aSource = effectsASourceQueue.Dequeue();
			if (aSource.isPlaying) aSource.Stop();
			aSource.clip = clip;
			aSource.Play();
			effectsASourceQueue.Enqueue(aSource);
		}
		else
		{
			Debug.Log("<color=yellow>Warning: too many sound effects at once, all audiosources are in use</color>");
		}
	}
	

	/// <summary>
	/// Plays a music clip(previously stored in an array)  in loop until another music clip starts.
	/// </summary>
	/// <param name="ID">Identifier of the music clip to play</param>
	public void PlayMusic (ushort ID)
	{
		PlayMusic(ID, false, 0);
	}

	public void PlayMusic (ushort ID, bool fadeIn, float time)
	{
		if (musicASource.isPlaying)
		{
			musicASource.Stop();
		}
		musicASource.clip = musicClips[ID];
		musicASource.loop = true;
		if (fadeIn)
		{
			musicASource.volume = 0;
			MusicFadeIn(time);
		}
		else
		{
			musicASource.volume = OptionsManager.Instance.GetMusicVolume();
		}
		musicASource.Play();
	}

	public void StopMusic ()
	{
		musicASource.Stop();
	}

	public void PauseMusic (bool paused)
	{
		if (paused) musicASource.Pause();
		else musicASource.UnPause();
	}

	public void MusicFadeIn (float time)
	{
		musicFadeIn = true;
		musicFadeOut = false;
		float target = OptionsManager.Instance.GetMusicVolume();
		fadeStep = target / (time / Time.fixedDeltaTime);
	}

	public void MusicFadeOut (float time)
	{
		musicFadeOut = true;
		musicFadeIn = false;
		fadeStep = -musicASource.volume / (time / Time.fixedDeltaTime);
	}
}
