using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInputController : InputController {

	private Camera mCamera;

	void Start ()
	{
		mCamera = Camera.main;
	}
	
	void Update ()
	{
		
	}

	public override bool ThrustersBurning ()
	{
		return Input.GetKey(KeyCode.Space);
	}

	public override bool ChangeDirection ()
	{
		return Input.GetMouseButton(0);
	}

	public override Vector2 GetDirection ()
	{
		Vector3 point = mCamera.ScreenToWorldPoint(Input.mousePosition);
		return (point - mCamera.transform.position).normalized;
	}
}
