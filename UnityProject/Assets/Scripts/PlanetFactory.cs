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
        GameObject planet = null;

        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        planet = GameObject.Instantiate(prefab, position, rotation);

        if(planet != null)
        {
            Planet pl = planet.GetComponent<Planet>();
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
        }

        return planet;
    }
}
