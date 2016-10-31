﻿
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour{
	GameObject[] pauseObjects;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
	}

	// Update is called once per frame
	void Update () {
		//uses the p button to pause and unpause the game
		if(Input.GetButton("Pause"))
		{
			pauseControl();
		}
	}


	//Reloads the Level
	public void Reload(){
		 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//controls the pausing of the scene
	public void pauseControl(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	public void Exit(){
		Application.Quit();
	}
}