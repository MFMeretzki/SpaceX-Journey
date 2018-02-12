using UnityEngine;

public class CollectableBarrel : CollectableItem
{
    protected override void PickItem ()
    {
        GameObject.Destroy(this.gameObject);
    }
}
