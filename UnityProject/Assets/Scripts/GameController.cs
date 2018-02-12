using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Transform ship;
    public Vector3 ShipPosition
    {
        get { return ship.position;  }
    }
    private Planet planet;
    public Planet Planet
    {
        get { return planet; }
        set { planet = value; }
    }

    public void Start () { }
    public void Update () { }
}
