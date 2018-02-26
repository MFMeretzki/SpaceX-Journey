using UnityEngine;
using UnityEngine.UI;

public class FuelIndicator : MonoBehaviour
{
    GameController gameController;
    float inverseFuelCapacity;

    [SerializeField]
    Image fillBar;

    protected void Awake ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inverseFuelCapacity = 1.0f / gameController.FuelCapacity;
    }

    protected void OnEnable ()
    {
        gameController.FuelChange += OnFuelChanges;
    }

    protected void Start () { }
    void Update () { }

    protected void OnDisable ()
    {
        gameController.FuelChange -= OnFuelChanges;
    }

    private void OnFuelChanges (float fuel)
    {
        fillBar.fillAmount = fuel * inverseFuelCapacity;
    }
}
