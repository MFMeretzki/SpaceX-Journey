using UnityEngine;

public class PauseMenuController : MonoBehaviour {

    protected void Awake () { }
    protected void OnEnable () { }
    protected void Start () { }
    protected void Update () { }
    protected void OnDisable () { }

    public void ResumePressed ()
    {
        bool p = !GameController.Paused;
        GameController.Pause(p);
        this.gameObject.SetActive(p);
    }
}
