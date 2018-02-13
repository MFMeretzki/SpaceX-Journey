using UnityEngine;

public class CameraController : MonoBehaviour
{

	public float SPEED_REDUCTION_GAP;
	public float ZOOM_ACC_COEFICIENT;
	public float SCALE_VEL_COEFICIENT;

	public GameController gameController;
	public Transform playerT;
	public float minZoom;
	public float maxZoom;
	public float minZoomDist;
	public float maxZoomDist;
	public float maxZoomVel;

	private Camera mCamera;
	private Planet planet = null;
	private float zoomVel;
	private float previousDist;

	void Start ()
	{
		mCamera = GetComponent<Camera>();
		zoomVel = 0;
	}

	void Update ()
	{
		Vector3 pos = mCamera.transform.position;
		pos.x = playerT.position.x;
		pos.y = playerT.position.y;
		mCamera.transform.position = pos;
	}

	void FixedUpdate ()
	{
		//calculate zoom
		planet = gameController.Planet;
		float distance;
		if (planet != null)
		{
			distance = (planet.transform.position - playerT.position).magnitude - planet.planetProperties.planetRadius - minZoomDist;
		}
		else distance = maxZoomDist;
		if (distance <= maxZoomDist)
		{
			float zoomD = minZoom - maxZoom;
			float objZoom = (distance * zoomD) / (maxZoomDist - minZoomDist) + maxZoom;
			float f = objZoom - mCamera.orthographicSize;
			zoomVel = zoomVel * 0.99f;
			if (Mathf.Abs(f) <= SPEED_REDUCTION_GAP)
			{
				float maxV = (Mathf.Abs(f) / SPEED_REDUCTION_GAP)*maxZoomVel;
				if (Mathf.Abs(zoomVel) > maxV) zoomVel = Mathf.Sign(zoomVel)*maxV;
			}
			else
			{
				zoomVel += f * Time.fixedDeltaTime * ZOOM_ACC_COEFICIENT;
			}
			if (Mathf.Abs(zoomVel) > maxZoomVel)
			{
				zoomVel = Mathf.Sign(zoomVel) * maxZoomVel;
			}
			mCamera.orthographicSize += zoomVel;
			if (mCamera.orthographicSize < maxZoom) mCamera.orthographicSize = maxZoom;
			else if (mCamera.orthographicSize > minZoom) mCamera.orthographicSize = minZoom;
		}
	}

}
