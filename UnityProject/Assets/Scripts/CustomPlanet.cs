using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlanet : Planet {
    
    [SerializeField]
    public float Mass;
    [SerializeField]
    public float Density;
    [SerializeField]
    public Color color;
    [SerializeField]
    public Color backgroundColor;
    [SerializeField]
    public Texture2D texture;

    private void Awake ()
    {
        Planet.Properties prop = new Planet.Properties(Mass, Density);
        this.SetProperties(prop, color, backgroundColor, texture);
    }

}
