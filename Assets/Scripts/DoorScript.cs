using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private BoxCollider2D boxColliderComponent;
	private Animator animatorComponent;
	public bool open = false;
	private SpriteRenderer[] spriteRenderers;

	// GameObject which plays sound effects
    private GameObject soundEffectsHelper;

	// Initialize variables and set the starting door state
	void Start(){
		soundEffectsHelper = GameObject.Find("soundEffectsHelper");

		animatorComponent = GetComponentInChildren<Animator>();

		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

		if(open){
			Open();
			ChangeSortingOrder();
		}
		else
			Close();
	}

	// On lever activation change door state
	void Activate(){
		open = !open;
		if(open){
			Open();
			soundEffectsHelper.SendMessage("DoorOpenSound");
		}
		else{
			Close();
			Invoke("CloseSound", 1);
		}

	    // Change sorting order after a small delay for the animation to catch up
		Invoke("ChangeSortingOrder", 1);
	}

	void CloseSound(){
		soundEffectsHelper.SendMessage("DoorCloseSound");
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
