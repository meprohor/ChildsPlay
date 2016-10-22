using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour {

	public GameObject affected;

	void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<SpriteRenderer>().flipX = false;
        affected.SendMessage("Activate");
        Destroy(GetComponent<BoxCollider2D>());
    }
}
