using UnityEngine;

public class Follower : MonoBehaviour {

    public Transform positionToMove;

    public float speed;
	
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, positionToMove.position, speed * Time.fixedDeltaTime);
    }
}
