using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float ZOOM_ACC_COEFICIENT;
	public float SCALE_VEL_COEFICIENT;

	public Transform playerT;
	public float minZoom;
	public float maxZoom;
	public float minZoomDist;
	public float maxZoomDist;
	public float mazZoomVel;

	private Camera mCamera;

	void Start ()
	{
		mCamera = GetComponent<Camera>();
	}

	void Update ()
	{
		Vector3 pos = mCamera.transform.position;
		pos.x = playerT.position.x;
		pos.y = playerT.position.y;
		mCamera.transform.position = pos;
	}

}
