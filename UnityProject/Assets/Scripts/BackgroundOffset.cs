using UnityEngine;

public class BackgroundOffset : MonoBehaviour {
    
    [SerializeField]
    Renderer backgroudRenderer;
    [SerializeField]
    Vector2 scale;
    
	void Start () {	}
	void Update ()
    {
        backgroudRenderer.material.SetVector("_ScaleOffset", new Vector4(
            transform.position.x,
            transform.position.y,
            scale.x,
            scale.y
            ));
    }
}
