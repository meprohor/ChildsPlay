using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject followedObject;
	private Vector3 cameraPosition;
	private Vector3 objectPosition;
	private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = followedObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		cameraPosition = transform.position;
		objectPosition = rigidBody.position;
		float x = followedObject.transform.position.x;

		Debug.Log("camera: " + cameraPosition.x);
		Debug.Log("x: " + x);
		Debug.Log("object: " + objectPosition.x);
		if(Mathf.Abs(cameraPosition.x - objectPosition.x) > 2.5f){
			if(cameraPosition.x > objectPosition.x)
				cameraPosition.x-=0.25f;
			else
				cameraPosition.x+=0.25f;
		}
		if(followedObject.GetComponent<PlayerScript>().canJump && Mathf.Abs(cameraPosition.y - objectPosition.y) > 0.25f){
			if(cameraPosition.y > objectPosition.y)
				cameraPosition.y-=0.25f;
			else
				cameraPosition.y+=0.25f;
		}
		transform.position = cameraPosition;
	}
}
