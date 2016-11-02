using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour {
	/* Game objects affected by pressing of this lever */
	public GameObject[] affected;

	/* Is the lever pressed? */
	public bool activated = false;

	/* Without this you will be pressing lever each frame you hold the button, 
	   Which is really fast */
	private float timeBetweenPresses = 0.3333f;
	private float timestamp;

	/* Animation stuff */ 
	private Animator animatorComponent;

	void Start(){
		animatorComponent = GetComponentInChildren<Animator>();
	}

	void OnTriggerStay2D(Collider2D other)
    {
    	/* Check if it is player who's touching the lever */
    	if(other.gameObject.CompareTag("Player")){
    		/* Check if the right amount of time has passed since you pulled the lever
    			and check if the player pressed the right button */
    		if(Time.time >= timestamp && Input.GetButton("Use")){
    			/* Update the timestamp */
    			timestamp = Time.time + timeBetweenPresses;
    			
    			/* Send activation message to all of the objects */
    			foreach(GameObject affectedObject in affected){
        	   	 	affectedObject.SendMessage("Activate");
        		}

        		/* Change activation state and set animation parameter */ 
        		activated = !activated;
        		animatorComponent.SetBool("Activated", activated);
        	}
    	}
    }
}
