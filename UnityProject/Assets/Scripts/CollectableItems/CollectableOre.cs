using UnityEngine;

public class CollectableOre : CollectableItem
{
    protected int ore;

    public override void Awake ()
    {
        base.Awake();
        ore = Random.Range(1, 4);
    }

    protected override void PickItem ()
    {
        gameController.OreCollected(ore);
        GameObject.Destroy(this.gameObject);
    }
}
