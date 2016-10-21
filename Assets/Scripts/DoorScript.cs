using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	public bool activated = false;

	void Open(){
		Destroy(this.gameObject);
	}
}
