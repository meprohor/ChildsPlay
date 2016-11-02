using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private BoxCollider2D boxColliderComponent;
	private Animator animatorComponent;
	public bool open = false;

	void Start(){
		animatorComponent = GetComponentInChildren<Animator>();
		boxColliderComponent = GetComponent<BoxCollider2D>();
		if(open)
			Open();
		else
			Close();
	}

	void Activate(){
		open = !open;
		if(open)
			Open();
		else
			Close();
	}

	void Open(){
		boxColliderComponent.enabled = false;
		animatorComponent.SetBool("Open", true);
	}

	void Close(){
		boxColliderComponent.enabled = true;
		animatorComponent.SetBool("Open", false);
	}
}
