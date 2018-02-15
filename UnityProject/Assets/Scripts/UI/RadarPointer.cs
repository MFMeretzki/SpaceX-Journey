using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadarPointer : UIBehaviour {

    GameController gameController;
    [SerializeField]
    RadarThreshold radarThreshold;
    Image image;

    protected override void Awake ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        image = GetComponent<Image>();
    }
    protected override void Start () { }
    protected void Update () { }

    public void SetObjective (Collider2D objective)
    {
        Vector2 dir = (objective.transform.position - gameController.Ship.position);

        if(dir.sqrMagnitude > 9.0f)
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
}
