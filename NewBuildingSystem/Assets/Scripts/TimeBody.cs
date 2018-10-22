using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {


    private bool isRewinding = false;
    private List<PointInTime> pointsInTime;
    public float recordTime = 5f;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.R)) {
            StopRewind();
        }
	}

    void FixedUpdate() {
        if (isRewinding) {
            Rewind();
        } else {
            if (rb.velocity != Vector3.zero) Record();
        }
    }

    private void Rewind() {
        if (pointsInTime.Count > 0) {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        } else {
            StopRewind();
        }
    }

    private void Record() {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    private void StartRewind() {
        isRewinding = true;
        rb.isKinematic = true;
    }

    private void StopRewind() {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
