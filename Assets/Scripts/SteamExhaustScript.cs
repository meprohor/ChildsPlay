using UnityEngine;
using System.Collections;

public class SteamExhaustScript : MonoBehaviour {
	public bool activated = false;
	public float velocity = 50;

	[HideInInspector]
    public int choiceIndex = 0;

    public GameObject prefab;

	void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && activated){
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
    	if(choiceIndex == 1){
  			InvokeRepeating("CreatePlatform", 0.0f, 15.0f);
            gameObject.tag = "Platform";
        }
  		else{
  			activated = true;
  			gameObject.tag = "Untagged";	
  		}
    }

    void CreatePlatform(){
    	Vector3 pos = transform.position;
    	pos.y += GetComponent<BoxCollider2D>().bounds.size.y;
    	Instantiate(prefab, pos, transform.rotation);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
