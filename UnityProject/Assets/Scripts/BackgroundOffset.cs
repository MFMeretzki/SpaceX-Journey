using UnityEngine;

public class BackgroundOffset : MonoBehaviour {
    
    [SerializeField]
    private Renderer backgroudRenderer;
    [SerializeField]
    private Vector2 scale;
	[SerializeField]
	private Camera mCamera;

	private CameraController cameraClr;
	private Vector2 offset;
	private Vector3 lastPos;

	void Start ()
	{
		cameraClr = mCamera.GetComponent<CameraController>();
	}

	void Update ()
    {
		float zoomFactor = (mCamera.orthographicSize - cameraClr.maxZoom) / (cameraClr.minZoom - cameraClr.maxZoom) + 0.3f;
		if (zoomFactor > 1f) zoomFactor = 1f;

		Vector2 posGap = transform.position - lastPos;
		offset.x += posGap.x * zoomFactor;
		offset.y += posGap.y * zoomFactor;

		backgroudRenderer.material.SetVector("_ScaleOffset", new Vector4(
            //transform.position.x,
            //transform.position.y,
			offset.x,
			offset.y,
			scale.x,
			scale.y
            ));

		lastPos = transform.position;
    }
}
