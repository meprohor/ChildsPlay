using UnityEngine;
using System.Collections;

public class PoliceDroneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If player touched the drone, trigger game over
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.SendMessage("GameOver");
			GameObject.FindWithTag("GameOverMenu").GetComponent<GameOverScript>().Invoke("GameOver", 1.10f);
		}
	}

	// If lever was pushed, deactivate the Drone
	void Activate(){
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<BoxCollider2D>().isTrigger = false; 
	}
}
