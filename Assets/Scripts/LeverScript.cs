using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour {

	public GameObject affected;

	void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<SpriteRenderer>().flipX = false;
        if(affected.CompareTag("Door"))
        	affected.SendMessage("Open");
        Destroy(GetComponent<BoxCollider2D>());
    }
}
