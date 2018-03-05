using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Color[] colors;
    [SerializeField]
    private Color[] backgrounds;
    [SerializeField]
    private Texture2D[] textures;

    [SerializeField]
    private GameObject ore;
    [SerializeField]
    private GameObject barrel;

    private int numColors;
    private int numBackgrounds;
    private int numTextures;

    [SerializeField]
    private Vector2 massThreshold;
    [SerializeField]
    private Vector2 densityThreshold;

    public void Awake ()
    {
        numColors = colors.Length;
        numBackgrounds = backgrounds.Length;
        numTextures = textures.Length;
    }

    public void Start () { }
    public void Update () { }

    /// <summary>
    /// Returns a new random generated planet located at given absolute position
    /// </summary>
    /// <param name="position">Position in wich planet wil be generated</param>
    /// <returns>Random generated planet</returns>
    public GameObject BuildPlanet (Vector3 position)
    {
        GameObject go = null;

        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        go = GameObject.Instantiate(prefab, position, rotation);

        if(go != null)
        {
            Planet pl = go.GetComponent<Planet>();
            Planet.Properties prop = new Planet.Properties(
                Random.Range(massThreshold.x, massThreshold.y),
                Random.Range(densityThreshold.x, densityThreshold.y)
                );

            pl.SetProperties(
                prop,
                colors[Random.Range(0, numColors)],
                backgrounds[Random.Range(0, numBackgrounds)],
                textures[Random.Range(0, numTextures)]
                );
            SetResources(go, prop.planetRadius);
        }

        return go;
    }

    private void SetResources(GameObject planet, float planetRadius)
    {
        int oreNum = Random.Range(0, 3);
        int barrelNum = Random.Range(0, 4);
        Queue<float> angles = GenerateAngles(oreNum + 1);

        float angle;
        Vector3 position;
        Quaternion rotation;
        GameObject go;
        for (int i=0; i<oreNum; ++i)
        {
            angle = angles.Dequeue();
            position = new Vector3(
                Mathf.Cos(angle) * (planetRadius + 0.025f),
                Mathf.Sin(angle) * (planetRadius + 0.025f),
                -0.5f
                );
            rotation = Quaternion.Euler(0.0f, 0.0f, (angle * Mathf.Rad2Deg) - 80.0f);
            go = GameObject.Instantiate(ore, position, rotation);
            go.transform.SetParent(planet.transform, false);
        }

        /*
        for (int i = 0; i < barrelNum; ++i)
        {
            angle = angles.Dequeue();
            position = new Vector3(
                Mathf.Cos(angle) * (planetRadius + 0.03f),
                Mathf.Sin(angle) * (planetRadius + 0.03f),
                -0.5f
                );
            rotation = Quaternion.Euler(0.0f, 0.0f, (angle * Mathf.Rad2Deg) - 90.0f);
            go = GameObject.Instantiate(barrel, position, rotation);
            go.transform.SetParent(planet.transform, false);
        }
        */
        if (barrelNum == 0)
        {
            angle = angles.Dequeue();
            position = new Vector3(
                Mathf.Cos(angle) * (planetRadius + 0.03f),
                Mathf.Sin(angle) * (planetRadius + 0.03f),
                -0.5f
                );
            rotation = Quaternion.Euler(0.0f, 0.0f, (angle * Mathf.Rad2Deg) - 90.0f);
            go = GameObject.Instantiate(barrel, position, rotation);
            go.transform.SetParent(planet.transform, false);
        }
    }

    private Queue<float> GenerateAngles(int num)
    {
        Queue<float> angles = new Queue<float>();

        for (int i=0; i<num; ++i)
        {
            angles.Enqueue(Random.Range(0.0f, Mathf.PI * 2.0f));
        }

        return angles;
    }
}
