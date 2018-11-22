using UnityEngine;

public class ObjectMaterialChanger : MonoBehaviour {

    private Renderer GameObjectRenderer;

    public Material materialHovered;
    public Material materialSelected;
    public Material materialDefault;

    private void Start() {
        GameObjectRenderer = gameObject.transform.GetComponent<Renderer>();
    }

    public void Hover() {
        if (GameObjectRenderer != null) {
            GameObjectRenderer.material = materialHovered;
        }
    }

    public void Select() {
        if (GameObjectRenderer != null) {
            GameObjectRenderer.material = materialSelected;
        }
    }

    public void Default() {
        if (GameObjectRenderer != null) {
            GameObjectRenderer.material = materialDefault;
        }
    }
}
