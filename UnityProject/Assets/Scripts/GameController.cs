using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Transform ship;

    public void Start () { }
    public void Update () { }

    public Vector3 GetShipPosition ()
    {
        return new Vector3(ship.position.x, ship.position.y, ship.position.z);
    }
}
