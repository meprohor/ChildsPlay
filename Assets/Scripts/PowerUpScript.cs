using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
    private PlayerScript player;

    [HideInInspector]
    public int choiceIndex = 0;

    void OnCollisionEnter2D(Collision2D collider)
    {
    	if(collider.gameObject.CompareTag("Player")){
    		player = collider.gameObject.GetComponent<PlayerScript>();
        	switch (choiceIndex){
            	case 0: player.unlockJump = true; break;
            	case 1: player.unlockTurn = true; break;
            	case 2: player.unlockPush = true; break;
            	case 3: player.unlockDoubleJump = true; break;
        	}
    	}
        Destroy(gameObject);
    }
}
