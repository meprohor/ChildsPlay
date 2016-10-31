using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject followedObject;
	private Vector3 cameraPosition;
	private Vector3 objectPosition;
	private Rigidbody2D rigidBody;
	private Rigidbody2D camRigidBody;

	public Vector2 offset = new Vector2(2.0f, 0.15f);
	public Vector2 cameraSpeed = new Vector2(50, 50);

	private Vector3 desiredVelocity;
	// Use this for initialization
	void Start () {
		rigidBody = followedObject.GetComponent<Rigidbody2D>();
		camRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		// Get camera position and followed object position
		cameraPosition = transform.position;
		objectPosition = rigidBody.position;
		
		// Check if the object moved beyond the specified offset
		if(Mathf.Abs(cameraPosition.x - objectPosition.x) > offset.x){
			// Set camera velocity vector so that it would followed the object horizontally
			Vector3 directionalVector = (objectPosition - cameraPosition).normalized * cameraSpeed.x;
			desiredVelocity.x = directionalVector.x;
		}else{
			// Otherwise set the velociy to zero
			desiredVelocity.x = 0;
		}

		// Move the camera vertically only if an object jumped on a higher platform or exceeded the specified vertical speed limit
		if((followedObject.GetComponent<PlayerScript>().isGrounded && Mathf.Abs(cameraPosition.y - objectPosition.y) > offset.y) || Mathf.Abs(rigidBody.velocity.y) > 35){
			// Set camera velocity vector so that it would followed the object vertically
			Vector3 directionalVector = (objectPosition - cameraPosition).normalized * cameraSpeed.y;
			desiredVelocity.y = directionalVector.y;
		}else{
			// Otherwise set it to zero
			desiredVelocity.y = 0;
		}
	}

	void FixedUpdate () {
		// Change camera's rigid body velocity
     	camRigidBody.velocity = desiredVelocity;
 }
}
