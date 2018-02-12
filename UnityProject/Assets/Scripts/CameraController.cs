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
	public float maxZoomVel;

	private Camera mCamera;
	public Planet planet;
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

		//calculate zoom
		if (planet != null)
		{
			float distance = (planet.transform.position - transform.position).magnitude; //- planet.radius;
			if (distance > minZoomDist && distance < maxZoomDist)
			{
				float zoomD = minZoom - maxZoom;
				float objZoom = (distance * zoomD) / maxZoomDist;
				float f = objZoom - mCamera.orthographicSize;
				zoomVel = zoomVel * 0.99f;
				if (Mathf.Abs(f) <= 0.5)
				{
					zoomVel = 0;
				}
				else if (Mathf.Abs(f) <= 1)
				{
					zoomVel -= Mathf.Sign(f)*Time.deltaTime*ZOOM_ACC_COEFICIENT;
				}
				else
				{
					zoomVel += f * Time.deltaTime * ZOOM_ACC_COEFICIENT;
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


	public void SetPlanet (Planet p)
	{
		planet = p;
		previousDist = (planet.transform.position - transform.position).magnitude;
		zoomVel = 0;
	}

}
