using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Transform ship;
    public Transform Ship
    {
        get { return ship;  }
    }
    public Planet Planet { get; set; }

    [SerializeField]
    private float fuelCapacity;
    private float fuel;

    public void Awake ()
    {
        Planet = null;
        fuel = fuelCapacity;
    }

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
