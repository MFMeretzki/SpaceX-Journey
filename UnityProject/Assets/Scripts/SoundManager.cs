using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	[SerializeField]
	private AudioSource musicASource;
	[SerializeField]
	private AudioSource[] effectsASource;
	private Queue<AudioSource> effectsASourceQueue;
	[SerializeField]
	private AudioClip[] musicClips;


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

	public void SetEffectVolume (float effectVolume)
	{
		for (int i = 0; i < effectsASource.Length; ++i)
		{
			effectsASource[i].volume = effectVolume;
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
			aSource.clip = clip;
			CoroutinePlayEffect(aSource);
		}
		else
		{
			Debug.Log("<color=yellow>Warning: too many sound effects at once, all audiosources are in use</color>");
		}
	}

	IEnumerator CoroutinePlayEffect (AudioSource source)
	{
		source.Play();
		yield return new WaitForSeconds(source.clip.length);
		effectsASourceQueue.Enqueue(source);
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
