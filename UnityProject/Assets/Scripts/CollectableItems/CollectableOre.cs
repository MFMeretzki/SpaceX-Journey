using UnityEngine;

public class CollectableOre : CollectableItem
{
    protected override void PickItem ()
    {
        GameObject.Destroy(this.gameObject);
    }
}
