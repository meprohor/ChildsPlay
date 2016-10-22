using UnityEngine;
using System.Collections;

public class SteamPlatformScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("DestroyPlatform", 5.0f);
	}
	
	void DestroyPlatform(){
		Destroy(gameObject);
	}

}
