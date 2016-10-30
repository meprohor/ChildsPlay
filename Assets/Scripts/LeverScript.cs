using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour {

	public GameObject affected;

	void OnCollisionEnter2D(Collision2D other)
    {
        affected.SendMessage("Activate");

        Animator animatorComponent = GetComponentInChildren<Animator>();
        animatorComponent.SetTrigger("Activated");

        Destroy(GetComponent<BoxCollider2D>());
    }
}
