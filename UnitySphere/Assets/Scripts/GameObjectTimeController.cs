using UnityEngine;

public class GameObjectTimeController : MonoBehaviour {

    private bool isTimeControllerActive = false;
    private Ray ray;
    public Transform raycastStartPosition;

    // Use this for initialization
    void Start () {
        ray = new Ray();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T)) isTimeControllerActive = !isTimeControllerActive;
        if (Input.GetKeyDown(KeyCode.G)) isTimeControllerActive = false;
        if (isTimeControllerActive) TimeController();
    }

    private void TimeController() {




        //Pause TimeBody
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //create Raycast
            ray.origin = raycastStartPosition.position;
            ray.direction = Camera.main.transform.forward;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject.GetComponent<TimeBody>()) {
                    Debug.Log("dsgsdfgsdfgsdfg");
                    hit.transform.gameObject.GetComponent<TimeBody>().Pause();
                }
            }
        }

        //Unpause TimeBody
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            //create Raycast
            ray.origin = raycastStartPosition.position;
            ray.direction = Camera.main.transform.forward;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject.GetComponent<TimeBody>()) {
                    Debug.Log("dsgsdfgsdfgsdfg");
                    hit.transform.gameObject.GetComponent<TimeBody>().Unpause();
                }
            }
        }
    }
}
