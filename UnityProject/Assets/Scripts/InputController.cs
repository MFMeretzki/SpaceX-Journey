using UnityEngine;

public abstract class InputController : MonoBehaviour {

	public abstract bool ThrustersBurning ();

	public abstract bool ChangeDirection ();
	public abstract Vector2 GetDirection ();

}
