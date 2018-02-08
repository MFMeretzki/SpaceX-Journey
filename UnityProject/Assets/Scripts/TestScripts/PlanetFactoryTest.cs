using UnityEngine;

public class PlanetFactoryTest : MonoBehaviour {

    [SerializeField]
    private PlanetFactory factory;
    
	public void Start () { }

	public void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            factory.BuildPlanet(this.transform.position);
        }
    }
}
