using UnityEngine;

public class Follower : MonoBehaviour {

    public Transform endMarker;

    public float speed;
	
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, endMarker.position, speed * Time.fixedDeltaTime);
    }
}
