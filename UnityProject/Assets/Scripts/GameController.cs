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

    [SerializeField]
    private float fuelCapacity;
    private float fuel;

    public void Start () { }
    public void Update () { }

    public void FuelConsumption(float volumeConsumed)
    {
        fuel -= volumeConsumed;
        if(fuel <= 0.0f)
        {
            // TO DO finish game
        }
    }
}
