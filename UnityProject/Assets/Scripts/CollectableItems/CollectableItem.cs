using UnityEngine;

public class CollectableItem : MonoBehaviour {

	[SerializeField]
	protected AudioClip pickSoundEffect;

    protected GameController gameController;

    public virtual void Awake ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void Start () { }
    public void Update () { }

    private void OnTriggerStay2D (Collider2D other)
    {
        Spaceship spaceship = other.gameObject.GetComponent<Spaceship>();
        if (spaceship != null)
        {
            if (spaceship.IsLanded())
            {
                PickItem();
            }
        }
    }

    protected virtual void PickItem ()
    {
		SoundManager.Instance.PlayEffect(pickSoundEffect);
    }
}
