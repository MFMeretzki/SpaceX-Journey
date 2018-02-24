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

		effectsASourceQueue = new Queue<AudioSource>();
		for (int i = 0; i < effectsASource.Length; ++i)
		{
			effectsASourceQueue.Enqueue(effectsASource[i]);
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
		if (musicASource.isPlaying)
		{
			musicASource.Stop();
		}
		musicASource.clip = musicClips[ID];
		musicASource.loop = true;
		musicASource.Play();
	}


}
