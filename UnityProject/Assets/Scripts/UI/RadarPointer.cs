using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadarPointer : UIBehaviour {

    private const float MAX_DIST_GRAD = 10.0f;

    GameController gameController;
    [SerializeField]
    RadarThreshold radarThreshold;
    Image image;
    Material material;

    float inverseMaxDistGrad = 0.99f / MAX_DIST_GRAD;
    

    protected override void Awake ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        image = GetComponent<Image>();
        material = Instantiate(image.material);
        image.material = material;
    }
    protected override void Start () { }
    protected void Update () { }

    public void SetObjective (Collider2D objective)
    {
        Vector2 dir = (objective.transform.position - gameController.Ship.transform.position);
        float dist = dir.magnitude;

        if(dist > 3.0f)
        {
            Vector2 thresholds = new Vector2(radarThreshold.Threshold[3], radarThreshold.Threshold[2]);
            Vector2 normalizedDir = dir.normalized;
            dir = normalizedDir * thresholds.magnitude;

            this.transform.localPosition = new Vector3(
                Mathf.Clamp(dir.x, radarThreshold.Threshold[1], radarThreshold.Threshold[3]),
                Mathf.Clamp(dir.y, radarThreshold.Threshold[0], radarThreshold.Threshold[2]),
                0.0f
                );
            float angle = Vector2.SignedAngle(normalizedDir, Vector2.up);
            Quaternion rot= Quaternion.Euler(0.0f, 0.0f, -angle);
            this.transform.rotation = rot;

            SetMaterialGradient(dist);

            SetVisible(true);
        }
        else
        {
            SetVisible(false);
        }
    }

    public void SetVisible(bool visible)
    {
        image.enabled = visible;
    }

    private void SetMaterialGradient(float dist)
    {
        float gradient;
        if (dist - 7.0f > MAX_DIST_GRAD)
        {
            gradient = 0.99f;
        }
        else if (dist - 7.0f < 0.0f)
        {
            gradient = 0.0f;
        }
        else
        {
            gradient = (dist - 7.0f) * inverseMaxDistGrad;
        }
        material.SetFloat("_Dist", gradient);
    }
}
