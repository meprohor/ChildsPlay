using UnityEngine;
using System.Collections;

public class KillZoneScript : MonoBehaviour {

	// Turn off sprite on start, we only need it for placement in editor
	void Start () {
		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// When player enters the killzone, GameOverMenu receives a message and stops the game
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player") && !other.isTrigger){
			 GameObject.FindWithTag("GameOverMenu").SendMessage("GameOver");
		}
	}
	
}
