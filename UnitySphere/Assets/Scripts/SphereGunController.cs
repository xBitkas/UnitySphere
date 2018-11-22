using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGunController : MonoBehaviour
{
    
    public bool gravitySphereIsActivated;
    public Transform hand;
    private Rigidbody objectInHand;
    public float throwForce;

    private Ray ray;
    public float maxRange;
    public Transform raycastStartPosition;
    public LayerMask interactionLayer;

    void Start()
    {
        ray = new Ray();
    }

    // Update is called once per frame
    void Update()
    {
        // Enter TimeSphere Mode! 
        if (Input.GetKeyDown(KeyCode.T)) {
            if (objectInHand != null) DropGameObject();
            gravitySphereIsActivated = false;
        }

        //Enter GravitySphere Mode!
        if (Input.GetKeyDown(KeyCode.G))
        {
            gravitySphereIsActivated = !gravitySphereIsActivated;
        }
        if (gravitySphereIsActivated)
        {
            activateGravitySphere();
        }
    }

    public void activateGravitySphere() {

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ray.origin = raycastStartPosition.position;
            ray.direction = Camera.main.transform.forward;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.cyan);
            RaycastHit hit;
            if (objectInHand == null) {
                if (Physics.Raycast(ray, out hit, maxRange, interactionLayer)) {
                    hit.transform.gameObject.GetComponent<TimeBody>().ResetPointsInTime();

                    objectInHand = hit.transform.GetComponent<Rigidbody>();
                    objectInHand.isKinematic = true;
                    objectInHand.transform.position = hand.position;
                    objectInHand.transform.parent = hand;
                }
            }
        }

        if (objectInHand != null && Input.GetKeyUp(KeyCode.Mouse0)) {
            DropGameObject();
        }

        if (objectInHand != null && Input.GetKeyDown(KeyCode.Mouse1)) {
            ShootGameObject();
        }
    }

    private void DropGameObject() {
        Debug.Log("Drop!");
        objectInHand.isKinematic = false;
        objectInHand.transform.parent = null;
        objectInHand = null;
    }

    private void ShootGameObject() {
        Debug.Log("Shoot!");
        objectInHand.isKinematic = false;
        objectInHand.transform.parent = null;
        objectInHand.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        objectInHand = null;
    }
}
