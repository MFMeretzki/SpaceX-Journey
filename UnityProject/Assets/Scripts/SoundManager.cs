using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	public const int THRUSTERS_ASOURCE_INDEX = GENERIC_ASOURCES;


	private const int GENERIC_ASOURCES = 4;
	

	[SerializeField]
	private AudioSource musicASource;
	[SerializeField]
	private AudioSource[] effectsASource;
	[SerializeField]
	private AudioClip[] musicClips;

	private Queue<AudioSource> effectsASourceQueue;
	private List<int> fadeIn;
	private List<int> fadeOut;
	private float musicVolume;
	private float effectsVolume;

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
		fadeIn = new List<int>();
		fadeOut = new List<int>();
		for (int i = 0; i < GENERIC_ASOURCES; ++i)
		{
			effectsASourceQueue.Enqueue(effectsASource[i]);
		}
	}

	void Start ()
	{
		musicVolume = OptionsManager.Instance.GetmusicVolume();
		effectsVolume = OptionsManager.Instance.GeteffectsVolume();

	}

	void FixedUpdate ()
	{
		for (int i = fadeIn.Count - 1; i >= 0; --i)
		{
			int index = fadeIn[i];
			if (effectsASource[index].volume < effectsVolume)
			{
				effectsASource[index].volume += 0.5f * Time.fixedDeltaTime;
			}
			else
			{
				fadeIn.RemoveAt(i);
			}
		}
		for (int i = fadeOut.Count - 1; i >= 0; --i)
		{
			int index = fadeOut[i];
			if (effectsASource[index].volume > 0)
			{
				effectsASource[index].volume -= 0.5f * Time.fixedDeltaTime;
			}
			else
			{
				effectsASource[index].Stop();
				fadeOut.RemoveAt(i);
			}
		}
	}


	public void SetMusicVolume (float musicVolume)
	{
		this.musicVolume = musicVolume;
		musicASource.volume = musicVolume;
	}

	public void SetEffectVolume (float effectsVolume)
	{
		this.effectsVolume = effectsVolume;
		for (int i = 0; i < GENERIC_ASOURCES; ++i)
		{
			effectsASource[i].volume = effectsVolume;
		}
	}

	public void LoopEffect (int index, AudioClip clip, bool fade)
	{
		effectsASource[index].clip = clip;
		effectsASource[index].loop = true;
		if (fade && !effectsASource[index].isPlaying)
		{
			effectsASource[index].volume = 0;
			fadeIn.Add(index);
		}
		else if (fade)
		{
			fadeIn.Add(index);
		}
		else
		{
			effectsASource[index].volume = effectsVolume;
		}
		if (effectsASource[index].isPlaying)
		{
			fadeOut.Remove(index);
		}
		effectsASource[index].Play();
	}

	public void StopLoopEffect (int index, bool fade)
	{
		fadeIn.Remove(index);
		if (fade)
		{
			fadeOut.Add(index);
		}
		else
		{
			effectsASource[index].Stop();
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
