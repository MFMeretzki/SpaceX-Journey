using UnityEngine;

public class Planet : MonoBehaviour {

    public struct Properties
    {
        public float mass;
        public float density;
        public float gravity;
        public float planetRadius;
        public float planetRadius2;
        public float gravityRadius;
        public float gravityRadius2;

        public Properties(float mass, float density)
        {
            this.mass = mass;
            this.density = density;
            this.gravity = mass * G_CONST;

            this.planetRadius2 = mass / density;
            this.planetRadius = Mathf.Sqrt(this.planetRadius2);

            this.gravityRadius2 = this.gravity * GRAVITY_RANGE_FACTOR;
            this.gravityRadius = Mathf.Sqrt(this.gravityRadius2);
        }
    }

	public Properties planetProperties;

	private const float G_CONST = 0.5f;
    private const float GRAVITY_RANGE_FACTOR = 30.0f;
    private const float EVENT_RADIUS = 10.0f;

    GameController gameController;

    [SerializeField]
    private CircleCollider2D surfaceCollider;
    [SerializeField]
    private CircleCollider2D eventsCollider;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject background;

    private float squareRootGravity;
    private float eventRadiusInverse = 1.0f / EVENT_RADIUS;

    public void Start ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        eventsCollider.enabled = true;
    }
    public void Update () { }

    private void OnTriggerEnter2D (Collider2D other)
    {
        Spaceship spaceship = other.GetComponent<Spaceship>();
        if (spaceship != null)
        {
            gameController.Planet = this;
        }
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        CosmicBody cosmicBody = other.GetComponent<CosmicBody>();
        if(cosmicBody != null)
        {
            cosmicBody.AddForce(ComputeForce(other.transform));
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        Spaceship spaceship = other.GetComponent<Spaceship>();
        if (spaceship != null)
        {
            gameController.Planet = null;
        }
    }

    public void SetProperties(Properties prop, Color color, Color background, Texture2D tex)
    {
        this.planetProperties = prop;

        this.model.transform.localScale = new Vector3(
            this.planetProperties.planetRadius, 
            this.planetProperties.planetRadius, 
            1.0f
            );

        this.background.transform.localScale = new Vector3(
            this.planetProperties.gravityRadius,
            this.planetProperties.gravityRadius,
            1.0f
            );
        this.eventsCollider.radius = EVENT_RADIUS;

        Renderer modelRend = this.model.GetComponent<Renderer>();
        modelRend.material.SetColor("_Color", color);
        modelRend.material.SetTexture("_MainTex", tex);
        Renderer backgroundRend = this.background.GetComponent<Renderer>();
        backgroundRend.material.SetColor("_Color", background);

        squareRootGravity = Mathf.Sqrt(planetProperties.gravity);

    }

    private Vector2 ComputeForce(Transform other)
    {
        Vector3 dir3d = this.transform.position - other.position;
        Vector2 dir = new Vector2(dir3d.x, dir3d.y);
        float f = (1 - (dir.magnitude * eventRadiusInverse)) * squareRootGravity;


        return dir.normalized * f * f;
    }
}
