using UnityEngine;

public class ObjectSelectionScript : MonoBehaviour {

    private bool isObjectSelectionActive = false;
    private Ray ray;
    public Transform raycastStartPosition;
    private GameObject gameObjectThatGotHit;
    private bool isGameObjectSelected = false;

	// Use this for initialization
	void Start () {
        ray = new Ray();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T)) isObjectSelectionActive = !isObjectSelectionActive;
        if (Input.GetKeyDown(KeyCode.G)) isObjectSelectionActive = !isObjectSelectionActive;
        if (!isObjectSelectionActive) {
            DeselectGameObject();
            return;
        }

        MouseInputHandler();
        if (isGameObjectSelected) return;
        RayHandler();
    }

    private void MouseInputHandler() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (gameObjectThatGotHit != null) {
                isGameObjectSelected = true;
                gameObjectThatGotHit.GetComponent<ObjectMaterialChanger>().Select();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (isGameObjectSelected == true) {
                isGameObjectSelected = false;
                if (gameObjectThatGotHit != null) gameObjectThatGotHit.GetComponent<ObjectMaterialChanger>().Default();
                gameObjectThatGotHit = null;
            }
        }
    }

    private void RayHandler() {
        ray.origin = raycastStartPosition.position;
        ray.direction = Camera.main.transform.forward;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.gameObject.GetComponent<TimeBody>()) {

                Debug.Log("got hit");

                if (gameObjectThatGotHit != hit.transform.gameObject && gameObjectThatGotHit != null) {
                    Debug.Log("if 1");
                    gameObjectThatGotHit.GetComponent<ObjectMaterialChanger>().Default();
                    gameObjectThatGotHit = null;
                }

                if (gameObjectThatGotHit != hit.transform.gameObject && gameObjectThatGotHit == null) {
                    Debug.Log("if 2");
                    gameObjectThatGotHit = hit.transform.gameObject;
                    gameObjectThatGotHit.GetComponent<ObjectMaterialChanger>().Hover();
                }
            } else {
                DeselectGameObject();
            }
        }
    }

    private void DeselectGameObject() {
        if (gameObjectThatGotHit != null) {
            gameObjectThatGotHit.GetComponent<ObjectMaterialChanger>().Default();
            gameObjectThatGotHit = null;
        }
    }
}
