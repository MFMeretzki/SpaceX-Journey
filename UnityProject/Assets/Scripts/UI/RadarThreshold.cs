using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadarThreshold : UIBehaviour {

    public enum Corner : int {
        bottom,
        left,
        top,
        right
    }
    
    GameController gameController;
    [SerializeField]
    RectTransform rectTransform;

    float[] threshold = new float[4];
    public float[] Threshold { get { return threshold; } }

    [SerializeField]
    private float radiusDetect;
    [SerializeField]
    private int maxNeighbourDectection;
    RadarPointer[] radarPointers;
    int radarPointersCount;
    CircleCollider2D[] neighbourPlanets;

    LayerMask mask;
    

    protected override void OnRectTransformDimensionsChange ()
    {
        threshold[0] = rectTransform.rect.min.y;
        threshold[1] = rectTransform.rect.min.x;
        threshold[2] = rectTransform.rect.max.y;
        threshold[3] = rectTransform.rect.max.x;
    }

    protected override void Awake ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        radarPointers = this.gameObject.GetComponentsInChildren<RadarPointer>(true);
        radarPointersCount = radarPointers.Length;
        neighbourPlanets = new CircleCollider2D[maxNeighbourDectection];
        mask = LayerMask.GetMask("Planet");
    }

    protected override void Start () { }

    protected void Update ()
    {
        int count = Physics2D.OverlapCircleNonAlloc(
            gameController.Ship.transform.position,
            radiusDetect, 
            neighbourPlanets,
            mask
            );

        int i;
        Collider2D[] neighbours = new Collider2D[count];
        for(i=0; i<count; ++i)
        {
            neighbours[i] = neighbourPlanets[i];
        }
        neighbours = neighbours.OrderBy(collider => (gameController.Ship.transform.position - collider.transform.position).sqrMagnitude).ToArray();

        i = 0;
        while (i < count && i < radarPointersCount)
        {
            radarPointers[i].SetObjective(neighbours[i]);
            i++;
        }
        while (i < radarPointersCount)
        {
            radarPointers[i].SetVisible(false);
            i++;
        }
    }
}
