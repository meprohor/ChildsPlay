using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private BoxCollider2D boxColliderComponent;
	private Animator animatorComponent;
	public bool open = false;
	private SpriteRenderer[] spriteRenderers;

	// Initialize variables and set the starting door state
	void Start(){
		animatorComponent = GetComponentInChildren<Animator>();

		if(open)
			Open();
		else
			Close();

		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}

	// On lever activation change door state
	void Activate(){
		open = !open;
		if(open)
			Open();
		else
			Close();
		// Change sorting order after a small delay for the animation to catch up
		Invoke("ChangeSortingOrder", 1);
	}

	// Open the door 
	void Open(){
		animatorComponent.SetBool("Open", true);
	}

	// Close the door
	void Close(){
		animatorComponent.SetBool("Open", false);
	}

	void ChangeSortingOrder(){
		foreach( SpriteRenderer sprite in spriteRenderers )
			if(open)
            	sprite.sortingOrder = sprite.sortingOrder - 20;
            else
            	sprite.sortingOrder = sprite.sortingOrder + 20;
	}
}
