using UnityEngine;
using System.Collections;

public class SteamExhaustScript : MonoBehaviour {
	public bool activated = false;
	public float velocity = 50;

	[HideInInspector]
    public int choiceIndex = 0;

    public GameObject prefab;

	void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && activated){
            other.gameObject.SendMessage("Jump", velocity);
        }
    }

    void Activate(){
    	if(choiceIndex == 1){
  			InvokeRepeating("CreatePlatform", 0.0f, 15.0f);
            gameObject.tag = "Platform";
        }
  		else
  			activated = true;	
    }

    void CreatePlatform(){
    	Instantiate(prefab, transform);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
