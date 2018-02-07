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

    private const float G_CONST = 1.0f;
    private const float GRAVITY_RANGE_FACTOR = 3.0f;

    [SerializeField]
    private CircleCollider2D surfaceCollider;
    [SerializeField]
    private CircleCollider2D eventsCollider;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private Properties planetProperties;
    private Color color;
    private Color backgroundColor;
    private Texture2D texture;

    void Start () { }
	void Update () { }

    public void SetProperties(Properties prop, Color color, Color background, Texture2D tex)
    {
        this.planetProperties = prop;

        this.model.transform.localScale = new Vector3(
            this.planetProperties.planetRadius, 
            this.planetProperties.planetRadius, 
            1.0f
            );
        this.surfaceCollider.radius = this.planetProperties.planetRadius;

        this.background.transform.localScale = new Vector3(
            this.planetProperties.gravityRadius,
            this.planetProperties.gravityRadius,
            1.0f
            );
        this.eventsCollider.radius = this.planetProperties.gravityRadius;

        Renderer modelRend = this.model.GetComponent<Renderer>();
        modelRend.material.SetColor("_Color", color);
        modelRend.material.SetTexture("_MainTex", tex);
        Renderer backgroundRend = this.background.GetComponent<Renderer>();
        backgroundRend.material.SetColor("_Color", background);
    }
}
