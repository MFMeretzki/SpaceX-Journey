using UnityEngine;

public class BackgroundOffset : MonoBehaviour {
    
    [SerializeField]
    Renderer renderer;
    [SerializeField]
    Vector2 scale;
    
	void Start () {	}
	void Update ()
    {
        renderer.material.SetVector("_ScaleOffset", new Vector4(
            transform.position.x,
            transform.position.y,
            scale.x,
            scale.y
            ));
    }
}
