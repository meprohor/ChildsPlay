using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {
	// store rigibody component
	private Rigidbody2D rigidbodyComponent;

	private SpriteRenderer spriteRendererComponent;
	private int orderOffset;

	// check if box is on the ground
	private bool isGrounded = true;
    private GameObject groundedOn = null;

    // play box sounds
	private GameObject soundEffectsHelper;

	// Use this for initialization
	void Start () {
		rigidbodyComponent = GetComponent<Rigidbody2D>();
		//rigidbodyComponent.isKinematic = true;
		/* С false значение body type в rigidbody устанавливается на dynamic и ящики падают сразу с инициализацией уровня.
			Почему небыло сделано так сразу, или почему не установаить dynamic в свойствах, зачем вообще Kinematic?
			Возможно, это одна из тех загадок, которые останутся без ответа.*/
		rigidbodyComponent.isKinematic = false;
		soundEffectsHelper = GameObject.Find("soundEffectsHelper");

		spriteRendererComponent = GetComponent<SpriteRenderer>();
		orderOffset = spriteRendererComponent.sortingOrder;
	}


	void OnCollisionEnter2D(Collision2D other){
		// Check if player touching the box has an ability to push it
		if(other.gameObject.CompareTag("Player")){
			if(other.gameObject.GetComponent<PlayerScript>().unlockPush)
				rigidbodyComponent.isKinematic = false;
		}
		// Check if the box is on the platform 
		if(other.gameObject.CompareTag("Platform")){
			isGrounded = true;
			groundedOn = other.gameObject;
			if(!isGrounded){
				soundEffectsHelper.SendMessage("BoxSound");
			}
		}

		if(other.gameObject.CompareTag("Door")){
			if(other.transform.position.x > transform.position.x){
    			spriteRendererComponent.sortingOrder += 10;
    		} 
		}

		if(other.gameObject.CompareTag("Box")){
			/*if(other.transform.position.y < transform.position.y){
    			spriteRendererComponent.sortingOrder += 1;
    		}*/
			//spriteRendererComponent.sortingOrder = (int)(transform.position.y);
			spriteRendererComponent.sortingOrder = (int)(transform.position.y*100 - transform.position.x*10);
			if (other.gameObject.name == "box (3)") {
				Debug.Log("Layer: " + transform.position.y + " " + transform.position.x);
			}
		}

	}

    // Check if box left the ground
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject == groundedOn){
            isGrounded = false;
            groundedOn = null;
        }

        if(other.gameObject.CompareTag("Box")){
			if(other.transform.position.y < transform.position.y){
    			//spriteRendererComponent.sortingOrder = orderOffset;
    		}
		}

		if(other.gameObject.CompareTag("Door")){
			if(other.transform.position.x > transform.position.x){
    			spriteRendererComponent.sortingOrder = orderOffset;
    		}
		}

    }
}
