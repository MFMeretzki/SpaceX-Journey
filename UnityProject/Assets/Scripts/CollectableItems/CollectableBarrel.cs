using UnityEngine;

public class CollectableBarrel : CollectableItem
{
    protected float fuel;

    public override void Awake ()
    {
        base.Awake();
        fuel = Random.Range(5.0f, 25.0f);
    }

    protected override void PickItem ()
    {
        gameController.FuelCollected(fuel);
        GameObject.Destroy(this.gameObject);
    }
}
