using UnityEngine;

public class BackgroundOffset : MonoBehaviour {
    
    [SerializeField]
    Renderer bgRenderer;
    [SerializeField]
    Vector2 scale;
    
	void Start () {	}
	void Update ()
    {
		bgRenderer.material.SetVector("_ScaleOffset", new Vector4(
            transform.position.x,
            transform.position.y,
            scale.x,
            scale.y
            ));
    }
}
