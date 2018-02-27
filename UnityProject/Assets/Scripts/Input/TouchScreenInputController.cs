using UnityEngine;
using TouchScript.Gestures;

public class TouchScreenInputController : InputController
{
    [SerializeField]
    DirectionGesture gesture;

    Camera mCamera;
    bool pressed = false;
    Vector2 pointer = Vector2.zero;


    private void OnEnable ()
    {
        gesture.Pressed += OnPressed;
        gesture.Moved += OnMoved;
        gesture.Released += OnReleased;
    }

    void Start ()
    {
        mCamera = Camera.main;
    }

	void Update () { }

    private void OnDisable ()
    {
        gesture.Pressed -= OnPressed;
        gesture.Moved -= OnMoved;
        gesture.Released -= OnReleased;
    }

    public override bool ThrustersBurning ()
    {
        return pressed;
    }

    public override bool ChangeDirection ()
    {
        return pressed;
    }

    public override Vector2 GetDirection ()
    {
        Vector3 point = mCamera.ScreenToWorldPoint(Input.mousePosition);
        return (point - mCamera.transform.position).normalized;
    }

    private void OnPressed (object sender, System.EventArgs e)
    {
        pressed = true;
    }

    private void OnMoved (object sender, System.EventArgs e)
    {
        pointer = gesture.PointerPosition;
    }

    private void OnReleased (object sender, System.EventArgs e)
    {
        pressed = false;
    }
}
