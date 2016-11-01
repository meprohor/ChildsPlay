using UnityEngine;
using System.Collections;

public class KillZoneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			 GameObject.FindWithTag("GameOverMenu").SendMessage("gameOver");
		}
	}
}
