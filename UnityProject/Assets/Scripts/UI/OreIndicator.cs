using UnityEngine;
using UnityEngine.UI;

public class OreIndicator : MonoBehaviour
{
    GameController gameController;

    [SerializeField]
    Text text;

    protected void Awake ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        OnOreChanges(0);
    }

    protected void OnEnable ()
    {
        gameController.OreChange += OnOreChanges;
    }

    protected void Start () { }
	void Update () { }

    protected void OnDisable ()
    {
        gameController.OreChange -= OnOreChanges;
    }

    private void OnOreChanges (int ore)
    {
        text.text = ore.ToString("D3");
    }
}
