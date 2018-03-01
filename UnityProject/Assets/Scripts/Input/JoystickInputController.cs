using UnityEngine;

public class JoystickInputController : InputController
{
    [SerializeField]
    JoystickGesture joystick;
    [SerializeField]
    PressButtonGesture button;
    
    bool moved = false;

    #region Unity methods
    private void OnEnable ()
    {
        joystick.Pressed += OnJoystickPressed;
        joystick.Released += OnJoystickReleased;
    }

    void Start () { }
    void Update () { }

    private void OnDisable ()
    {
        joystick.Pressed -= OnJoystickPressed;
        joystick.Released -= OnJoystickReleased;
    }
    #endregion

    public override bool ChangeDirection ()
    {
        return moved;
    }

    public override Vector2 GetDirection ()
    {
        return joystick.Direction;
    }

    public override bool ThrustersBurning ()
    {
        return button.ButtonPressed;
    }

    #region gesture callbacks
    private void OnJoystickPressed (object sender, System.EventArgs e)
    {
        moved = true;
    }

    private void OnJoystickReleased (object sender, System.EventArgs e)
    {
        moved = false;
    }
    #endregion
}
