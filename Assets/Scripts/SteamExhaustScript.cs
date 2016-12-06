using UnityEngine;
using System.Collections;

public class SteamExhaustScript : MonoBehaviour {
	public bool activated = false;
	public float velocity = 50;

	[HideInInspector]
    public int choiceIndex = 0;

    public GameObject prefab;

    private GameObject platform = null;

	void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && activated && choiceIndex == 0){
        	foreach (ContactPoint2D contact in other.contacts){
                if (contact.point.y > transform.position.y && contact.point.x >= transform.position.x - 1
                										   && contact.point.x < transform.position.x + GetComponent<BoxCollider2D>().bounds.size.x){
            		other.gameObject.SendMessage("Jump", velocity);
            		break;
                }
            }
        }
    }

    void Activate(){
        activated = !activated;
        if(activated){
            if(choiceIndex == 1){
                InvokeRepeating("CreatePlatform", 0.0f, 15.0f);
                gameObject.tag = "Platform";
            }
  		    else{
                gameObject.tag = "Untagged";	
  		    }
        }
        else{
            CancelInvoke();
            if(platform != null){
                platform.gameObject.SendMessage("DestroyPlatform");
                platform = null;
            }
        }
    }

    void CreatePlatform(){
    	Vector3 pos = transform.position;
    	pos.y += GetComponent<BoxCollider2D>().bounds.size.y+1;
    	platform = (GameObject)Instantiate(prefab, pos, transform.rotation);
    }
}
