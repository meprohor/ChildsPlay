using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject followedObject;
	private Vector3 cameraPosition;
	private Vector3 objectPosition;
	private Rigidbody2D rigidBody;
	private Rigidbody2D camRigidBody;

	private Vector3 desiredVelocity;
	// Use this for initialization
	void Start () {
		rigidBody = followedObject.GetComponent<Rigidbody2D>();
		camRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		cameraPosition = transform.position;
		objectPosition = rigidBody.position;
		
		/*if(Mathf.Abs(cameraPosition.x - objectPosition.x) > 2.5f){
			if(cameraPosition.x > objectPosition.x)
				cameraPosition.x-=0.25f;
			else
				cameraPosition.x+=0.25f;
		}*/
		
		if(Mathf.Abs(cameraPosition.x - objectPosition.x) > 2.0f){
			Vector3 directionalVector = (objectPosition - cameraPosition).normalized * 50;
			desiredVelocity.x = directionalVector.x;

		}else{
			desiredVelocity.x = 0;
		}

		/*if(followedObject.GetComponent<PlayerScript>().canJump && Mathf.Abs(cameraPosition.y - objectPosition.y) > 0.25f){
			if(cameraPosition.y > objectPosition.y)
				cameraPosition.y-=0.25f;
			else
				cameraPosition.y+=0.25f;
		}
		transform.position = cameraPosition;*/

		if(followedObject.GetComponent<PlayerScript>().isGrounded && Mathf.Abs(cameraPosition.y - objectPosition.y) > 0.25f){
			Vector3 directionalVector = (objectPosition - cameraPosition).normalized * 50;
			desiredVelocity.y = directionalVector.y;
		}else{
			desiredVelocity.y = 0;
		}
	}

	void FixedUpdate () {
     camRigidBody.velocity = desiredVelocity;
 }
}
