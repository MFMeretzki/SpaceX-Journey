using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Transform ship;
    public Transform Ship
    {
        get { return ship; }
    }
    public Planet Planet { get; set; }

[SerializeField]
    private float fuelCapacity;
    private float fuel;
    private int ore;

    public void Awake ()
    {
        Planet = null;
        fuel = fuelCapacity;
        ore = 0;
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

    public void FuelCollected (float fuelAmount)
    {
        fuel += fuelAmount;
        if (fuel > fuelCapacity) fuel = fuelCapacity;
    }

    public void OreCollected(int oreAmount)
    {
        ore += oreAmount;
    }
}
