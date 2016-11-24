using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitAreaScript : MonoBehaviour {
	public string nextScene;
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player") && !other.isTrigger){
 			SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
 		}
	}
}
