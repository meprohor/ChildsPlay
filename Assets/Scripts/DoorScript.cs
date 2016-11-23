using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private BoxCollider2D boxColliderComponent;
	private Animator animatorComponent;
	public bool open = false;
	public SpriteRenderer[] spriteRenderers;

	// Initialize variables and set the starting door state
	void Start(){
		animatorComponent = GetComponentInChildren<Animator>();

		if(open)
			Open();
		else
			Close();

		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		//foreach( SpriteRenderer sprite in spriteRenderers )
        //    sprite.sortingOrder = -1*(int)transform.position.x+sprite.sortingOrder;
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

		foreach( SpriteRenderer sprite in spriteRenderers )
            sprite.sortingOrder = sprite.sortingOrder - 10;
	}

	// Close the door
	void Close(){
		//boxColliderComponent.enabled = true; controlled by animator
		animatorComponent.SetBool("Open", false);

		foreach( SpriteRenderer sprite in spriteRenderers )
            sprite.sortingOrder = sprite.sortingOrder + 10;
	}
}
