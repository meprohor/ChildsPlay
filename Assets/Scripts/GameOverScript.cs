using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
	GameObject[] pauseObjects;
	public static bool IsGameOver;

	// Use this for initialization
	void Start () {
		IsGameOver = false;
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
		hideGameOver();
	}

	//Reloads the Level
	public void Reload(){
		 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//controls the pausing of the scene
	public void gameOver(){
		IsGameOver = true;
		Time.timeScale = 0;
		showOnGameOver();
	}

	//shows objects with ShowOnPause tag
	public void showOnGameOver(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hideGameOver(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	public void Exit(){
		Application.Quit();
	}
}
