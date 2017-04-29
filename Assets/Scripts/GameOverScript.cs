using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
	GameObject[] pauseObjects;
	public static bool IsGameOver;

	// Initialize variables and hide GameOverMenu
	void Start () {
		IsGameOver = false;
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
		HideGameOver();
	}

	// Reloads the Level
	public void Reload(){
		 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu(){
		 SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
	}

	// Controls the pausing of the scene
	public void GameOver(){
		IsGameOver = true;
		Time.timeScale = 0;
		ShowOnGameOver();
	}

	// Shows objects with ShowOnPause tag
	public void ShowOnGameOver(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	// Hides objects with ShowOnPause tag
	public void HideGameOver(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	// Exits the game 
	public void Exit(){
		Application.Quit();
	}
}
