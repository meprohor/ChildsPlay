using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour {

	public GameObject[] points;
	public float speed;
	
	private int currentObject;
	
	// Use this for initialization
	void Start () {
		currentObject = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, points[currentObject].transform.position, step);
		if (transform.position == points[currentObject].transform.position) {
			currentObject++;
			if (currentObject == points.Length) {
				currentObject = 0;
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		other.transform.SetParent(transform);
	}
	
	void OnCollisionExit2D(Collision2D other) {
		other.transform.SetParent(null);
	}
}