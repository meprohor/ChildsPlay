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

	void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")){
			bool flag = false;
			foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.point.y < transform.position.y){
                    flag = true;
                    break;
                }
            }
            if(flag)
				other.gameObject.SendMessage("Jump", 10.0f);
		}
	}
}
