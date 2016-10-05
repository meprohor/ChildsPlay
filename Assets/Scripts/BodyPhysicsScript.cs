using UnityEngine;
using System.Collections;

public class BodyPhysicsScript : MonoBehaviour {
	private Quaternion quaternionZero;
	public float t = .25f;
	
	void Start () {
		quaternionZero.z = 0.0f;
	}

	void FixedUpdate () {
		transform.rotation = Quaternion.Lerp (transform.rotation, quaternionZero, t);
	}
}
