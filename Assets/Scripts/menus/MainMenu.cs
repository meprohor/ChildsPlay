/* Button */
using UnityEngine.UI;
/* EventSystem */
using UnityEngine.EventSystems;
/* SceneManager */
using UnityEngine.SceneManagement;
/* ToList */
using System.Linq;
/* List */
using System.Collections.Generic;
/* Screen */
using UnityEngine;
/* AssetDatabase */
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class MainMenu
{
	private static GameObject _gameObject;
	public static GameObject gameObject
	{
		get
		{
			GameObject tempGO;
			
			if(null == _gameObject)
			{
				_gameObject = GameObject.Find("MainMenu");
			
				if(null == _gameObject)
				{
					_gameObject = new GameObject("MainMenu",
						typeof(Canvas),
						typeof(CanvasScaler),
						typeof(GraphicRaycaster));
					
					_gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
				}
				
				GameObject childGO;
				Button button = null;
				List<string> buttonsToAdd = new List<string>();
				buttonsToAdd.Add("Quit");
				buttonsToAdd.Add("Options");
				if(System.IO.File.Exists(Game.dataPath))
				{
					buttonsToAdd.Add("Continue");
					buttonsToAdd.Add("Previous Level");
					buttonsToAdd.Add("Load Game");
					buttonsToAdd.Add("Next Level");
					#if UNITY_EDITOR
					buttonsToAdd.Add("Delete Save");
					#endif
				}
				else
				{
					selectedLevel = "";
					_selectedLevel.gameObject.SetActive(false);
				}
				buttonsToAdd.Add("New Game");
				
				List<GameObject> excessiveButtons = new List<GameObject>();
				
				int count = buttonsToAdd.Count;
				
				foreach(Transform child in _gameObject.transform)
				{
					button = child.GetComponent<Button>();
					
					if(null != button)
						if(buttonsToAdd.Contains(child.name))
						{
							buttonsToAdd.Remove(child.name);
							ManageOnClick(child.name, button);
						}
						else
							excessiveButtons.Add(button.gameObject);
				}
				
				foreach(GameObject excessiveButton in excessiveButtons)
				{
					excessiveButton.SetActive(false);
				}
				
				int i = 0;
				Vector2 size = new Vector2(Screen.width / 5, Screen.height / Mathf.Max(10, count));
				foreach(string buttonName in buttonsToAdd)
				{
					childGO = new GameObject(buttonName);
					childGO.transform.SetParent(_gameObject.transform);
					
					Image tempI = childGO.AddComponent<Image>();
					#if UNITY_EDITOR
					tempI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
					#endif
					tempI.type = Image.Type.Sliced;
					
					button = childGO.AddComponent<Button>();
					ManageOnClick(buttonName, button);
					
					RectTransform tempRT = childGO.GetComponent<RectTransform>();
					tempRT.sizeDelta = size;
					Vector3 tempV3 = new Vector3((tempRT.sizeDelta.x - Screen.width) / 2,
						(tempRT.sizeDelta.y - Screen.height) / 2 + tempRT.sizeDelta.y * i,
						.0f);
					tempRT.anchoredPosition = tempV3;
					
					tempGO = new GameObject("Text");
					tempGO.transform.SetParent(childGO.transform);
					
					Text tempT = tempGO.AddComponent<Text>();
					tempT.text = buttonName;
					tempT.color = Color.black;
					tempT.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
					tempT.alignment = TextAnchor.MiddleCenter;
					
					tempRT = tempGO.GetComponent<RectTransform>();
					tempRT.anchoredPosition = Vector3.zero;
					tempRT.sizeDelta = size;
					++ i;
				}
				
				_gameObject.SetActive(false);
				
				tempGO = GameObject.Find("EventSystem");
				
				if(null == tempGO)
				{
					new GameObject("EventSystem",
						typeof(EventSystem),
						typeof(StandaloneInputModule));
				}
			}
			
			return _gameObject;
		}
	}
	
	private static void ManageOnClick(string buttonName, Button button)
	{
		switch(buttonName)
		{
			case "New Game":
				button.onClick.AddListener(() => OnNewGame());
				break;
			case "Options":
				button.onClick.AddListener(() => OnOptions());
				break;
			case "Continue":
				button.onClick.AddListener(() => OnContinue());
				break;
			case "Next Level":
				button.onClick.AddListener(() => OnNextLevel());
				break;
			case "Load Game":
				button.onClick.AddListener(() => OnLoadLevel());
				break;
			case "Previous Level":
				button.onClick.AddListener(() => OnPrevLevel());
				break;
			case "Quit":
				button.onClick.AddListener(() => OnQuit());
				break;
			case "Delete Save":
				button.onClick.AddListener(() => OnDeleteSave());
				break;
			default:
				break;
		}
	}
	
	private static Text _selectedLevel;
	private static string selectedLevel
	{
		set
		{
			if(null == _selectedLevel)
			{
				GameObject tempGO;
				tempGO = GameObject.Find("Selected Level");
				
				_selectedLevel = tempGO.GetComponent<Text>();
			}
			
			_selectedLevel.text = value;
		}
		get { return _selectedLevel.text; }
	}
	
	private static int _levelIndex = 0;
	private static int levelIndex
	{
		set
		{
			_levelIndex = Mathf.Clamp(value, 0, Game.unlockedLevels.Count() - 1);
			
			selectedLevel = Game.unlockedLevels[levelIndex];
		}
		get { return _levelIndex; }
	}
	
	public static void OnQuit()
	{
		Application.Quit();
	}
	
	public static void OnOptions()
	{
		SceneManager.LoadScene("Options_Menu", LoadSceneMode.Single);
	}
	
	public static void OnContinue()
	{
		SceneManager.LoadScene(Game.levelName, LoadSceneMode.Single);
	}
	
	public static void OnPrevLevel()
	{
		levelIndex --;
	}
	
	public static void OnNextLevel()
	{
		levelIndex ++;
	}
	
	public static void OnLoadLevel()
	{
		SceneManager.LoadScene(selectedLevel, LoadSceneMode.Single);
	}
	
	public static void OnNewGame()
	{
		SceneManager.LoadScene(MainMenuController.firstLevel, LoadSceneMode.Single);
	}
	
	public static void OnDeleteSave()
	{
		System.IO.File.Delete(Game.dataPath);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
}
