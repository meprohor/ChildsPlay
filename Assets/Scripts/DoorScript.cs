using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	//private BoxCollider2D boxColliderComponent;
	private Animator animatorComponent;
	public bool open = false;

	// Initialize variables and set the starting door state
	void Start(){
		animatorComponent = GetComponentInChildren<Animator>();
		//boxColliderComponent = GetComponent<BoxCollider2D>();
		if(open)
			Open();
		else
			Close();
	}

	// On lever activation change door state
	void Activate(){
		open = !open;
		if(open)
			Open();
		else
			Close();
	}

	// Open the door 
	void Open(){
		//boxColliderComponent.enabled = false; controlled by animator
		animatorComponent.SetBool("Open", true);
	}

	// Close the door
	void Close(){
		//boxColliderComponent.enabled = true; controlled by animator
		animatorComponent.SetBool("Open", false);
	}
}
