using UnityEngine;

public class CollectableBarrel : CollectableItem
{
    protected float fuel;

    public override void Awake ()
    {
        base.Awake();
        fuel = Random.Range(10.0f, 14.0f);
    }

    protected override void PickItem ()
    {
		base.PickItem();
        gameController.FuelCollected(fuel);
        GameObject.Destroy(this.gameObject);
    }
}
