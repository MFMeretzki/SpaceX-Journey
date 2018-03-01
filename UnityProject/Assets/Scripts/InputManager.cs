using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Spaceship ship;

    [SerializeField]
    private InputController[] controls;
    private Dictionary<string, InputController> controlsDic;

    private void Awake ()
    {
        controlsDic = new Dictionary<string, InputController>();
        controlsDic.Add("Joystick", controls[0]);
        controlsDic.Add("TouchnGo", controls[1]);
    }

    private void OnEnable ()
    {
        OptionsManager.Instance.ControlsChange += OnControlsChange;
    }

    void Start ()
    {
        OnControlsChange();
    }
	void Update () { }

    private void OnDisable ()
    {
        OptionsManager.Instance.ControlsChange -= OnControlsChange;
    }

    private void OnControlsChange ()
    {
        string c = OptionsManager.Instance.GetControls();
        foreach(var go in controlsDic)
        {
            if (go.Key == c)
            {
                go.Value.gameObject.SetActive(true);
                ship.inputC = go.Value;
            }
            else
            {
                go.Value.gameObject.SetActive(false);
            }
        }
    }
}
