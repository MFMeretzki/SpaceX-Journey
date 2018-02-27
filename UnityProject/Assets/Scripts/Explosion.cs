using UnityEngine;

public class Explosion : MonoBehaviour {

	public AudioClip explosionClip;
	public AudioSource audioSource;

	void Start ()
	{
		Camera mCamera = Camera.main;
		Vector3 pos = mCamera.WorldToViewportPoint(transform.position);
		if (pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1)
		{
			audioSource.clip = explosionClip;
			audioSource.volume = OptionsManager.Instance.GetEffectsVolume();
			audioSource.Play();
		}
	}


	public void DestroyObject ()
	{
		Destroy(gameObject);
	}
	
}
