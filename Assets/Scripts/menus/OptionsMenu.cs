/* Screen */
using UnityEngine;
/* Button */
using UnityEngine.UI;
/* EventSystem */
using UnityEngine.EventSystems;
/* SceneManager */
using UnityEngine.SceneManagement;
/* Serializable */
using System;
/* ToList */
using System.Linq;
/* List */
using System.Collections.Generic;
/* File */
using System.IO;
/* BinaryFormatter */
using System.Runtime.Serialization.Formatters.Binary;
/* AssetDatabase */
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class OptionsMenu
{
	private static GameObject _gameObject;
	public static GameObject gameObject
	{
		get
		{
			GameObject tempGO;
			
			if(null == _gameObject)
			{
				_gameObject = GameObject.Find("OptionsMenu");
			
				if(null == _gameObject)
				{
					_gameObject = new GameObject("OptionsMenu",
						typeof(Canvas),
						typeof(CanvasScaler),
						typeof(GraphicRaycaster));
					
					_gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
				}
				
				GameObject childGO;
				Button button = null;
				List<string> buttonsToAdd = new List<string>();
				buttonsToAdd.Add("Back");
				buttonsToAdd.Add("Toggle Mute");
				buttonsToAdd.Add("Less Volume");
				buttonsToAdd.Add("More Volume");
				buttonsToAdd.Add("Toggle Fullscreen");
				buttonsToAdd.Add("Prev Resolution");
				buttonsToAdd.Add("Next Resolution");
				buttonsToAdd.Add("Max Resolution");
				buttonsToAdd.Add("Default");
				buttonsToAdd.Add("Decline");
				buttonsToAdd.Add("Apply");
				
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
			case "Apply":
				button.onClick.AddListener(() => OnApplied());
				break;
			case "Decline":
				button.onClick.AddListener(() => OnDeclined());
				break;
			case "Default":
				button.onClick.AddListener(() => OnDefault());
				break;
			case "Max Resolution":
				button.onClick.AddListener(() => OnMaxResolution());
				break;
			case "Next Resolution":
				button.onClick.AddListener(() => OnNextResolution());
				break;
			case "Prev Resolution":
				button.onClick.AddListener(() => OnPrevResolution());
				break;
			case "Toggle Fullscreen":
				button.onClick.AddListener(() => OnToggleFullScreen());
				break;
			case "More Volume":
				button.onClick.AddListener(() => OnMoreVolume());
				break;
			case "Less Volume":
				button.onClick.AddListener(() => OnLessVolume());
				break;
			case "Toggle Mute":
				button.onClick.AddListener(() => OnToggleMute());
				break;
			case "Back":
				button.onClick.AddListener(() => OnBack());
				break;
			default:
				break;
		}
	}
	
	public static string dataPath
	{
		get { return Application.persistentDataPath + "/config.dat"; }
	}
	
	private static void Save()
	{
		BinaryFormatter tempBF = new BinaryFormatter();
		FileStream tempFS = File.Create(dataPath);
		
		Configs data = new Configs();
		data.fullScreen = fullScreen;
		data.resolutionWidth = curRes.width;
		data.resolutionHeight = curRes.height;
		data.mute = mute;
		data.volume = volume;
		
		tempBF.Serialize(tempFS, data);
		tempFS.Close();
	}
	
	public static void Load()
	{
		if(!File.Exists(dataPath))
		{
			mute = 1.0f;
			volume = 1.0f;
			AudioListener.volume = 1.0f;
			
			Save();
			return;
		}
		
		BinaryFormatter tempBF = new BinaryFormatter();
		FileStream tempFS = File.Open(dataPath, FileMode.Open);
		
		Configs data = (Configs)tempBF.Deserialize(tempFS);
		tempFS.Close();
		
		fullScreen = data.fullScreen;
		
		Resolution tempR = new Resolution();
		tempR.width = data.resolutionWidth;
		tempR.height = data.resolutionHeight;
		
		curRes = tempR;
		
		mute = data.mute;
		volume = data.volume;
	}
	
	private static Text _resolutionText;
	private static string resolutionText
	{
		set
		{
			if(null == _resolutionText)
			{
				GameObject tempGO;
				tempGO = GameObject.Find("Resolution");
				
				_resolutionText = tempGO.GetComponent<Text>();
			}
			
			_resolutionText.text = value;
		}
	}
	
	private static Text _volumeText;
	private static string volumeText
	{
		set
		{
			if(null == _volumeText)
			{
				GameObject tempGO;
				tempGO = GameObject.Find("Volume");
				
				_volumeText = tempGO.GetComponent<Text>();
			}
			
			_volumeText.text = value;
		}
	}
	
	private static List<float> aspectRatios
	{
		get { return OptionsMenuController.aspectRatios; }
	}
	
	private static Resolution minRes
	{
		get { return OptionsMenuController.minRes; }
	}
	
	private static bool _fullScreen = Screen.fullScreen;
	private static bool fullScreen
	{
		get { return _fullScreen; }
		set
		{
			if(value != _fullScreen)
			{
				_fullScreen = value;
				
				curResIndex = 0;
				curRes = resolutionsList[curResIndex];
				
				ManageResolutionText();
			}
		}
	}
	
	private static List<Resolution> _fullScreenResolutions;
	private static List<Resolution> fullScreenResolutions
	{
		get
		{
			if(null == _fullScreenResolutions)
			{
				_fullScreenResolutions = new List<Resolution>();
				float ratio;
				
				foreach(Resolution resolution in Screen.resolutions)
				{
					if(resolution.width <= minRes.width
						|| resolution.height <= minRes.height)
						continue;
					
					ratio = Mathf.Round(100.0f * ((float)resolution.width / (float)resolution.height)) / 100.0f;
					
					if(aspectRatios.Contains(ratio))
						_fullScreenResolutions.Add(resolution);
				}
			}
			
			return _fullScreenResolutions;
		}
	}
	
	private static List<Resolution> _windowedResolutions;
	private static List<Resolution> windowedResolutions
	{
		get
		{
			if(null == _windowedResolutions)
			{
				_windowedResolutions = OptionsMenuController.windowedResolutions;
			}
			
			return _windowedResolutions;
		}
	}
	
	private static List<Resolution> resolutionsList
	{
		get { return (fullScreen)?(fullScreenResolutions):(windowedResolutions); }
	}
	
	/* stands for current resolution index */
	private static int _curResIndex = -1;
	private static int curResIndex
	{
		get
		{
			if(-1 == _curResIndex)
			{
				Resolution tempR = Screen.currentResolution;
				bool isSet = false;
				
				foreach(Resolution resolution in resolutionsList)
				{
					if(resolution.width == tempR.width
						&& resolution.width == tempR.width)
					{
						isSet = true;
						break;
					}
				}
				
				if(false == isSet)
					resolutionsList.Add(tempR);
				
				int i = 0;
				
				foreach(Resolution resolution in resolutionsList)
				{
					if(resolution.width == tempR.width
						&& resolution.height == tempR.height)
					{
						_curResIndex = i;
						break;
					}
					
					i ++;
				}
			}
			
			return _curResIndex;
		}
		set
		{
			int count = resolutionsList.Count - 1;
		
			if(count <= value)
				_curResIndex = count;
			else if(0 >= value)
				_curResIndex = 0;
			else
				_curResIndex = value;
			
			ManageResolutionText();
		}
	}
	
	private static Resolution _curRes = new Resolution();
	private static Resolution curRes
	{
		get
		{
			_curRes = resolutionsList[curResIndex];
			
			return _curRes;
		}
		set
		{
			_curRes = value;
			
			ManageResolutionText();
		}
	}
	
	private static bool _mute;
	private static float mute
	{
		get { return (true == _mute)?(1.0f):(.0f); }
		set
		{
			_mute = (Mathf.Approximately(.0f, value))?(false):(true);
			
			ManageVolumeText();
		}
	}
	
	private static float _volume;
	private static float volume
	{
		get { return _volume; }
		set
		{
			if(1.0f <= value)
				_volume = 1.0f;
			else if(.0f >= value)
				_volume = .0f;
			else
				_volume = value;
			
			_volume = Mathf.Round(100.0f * _volume) / 100.0f;
			
			ManageVolumeText();
		}
	}
	private static float volumeStep
	{
		get { return OptionsMenuController.volumeStep; }
	}
	
	public static void ManageResolutionText()
	{
		string tempS = "";
		
		tempS = curRes.width + "X" + curRes.height + '\n';
		
		if(true == fullScreen)
			tempS += "fullscreen";
		else
			tempS += "windowed";
		
		resolutionText = tempS;
	}
	
	public static void ManageVolumeText()
	{
		string tempS = "Volume: ";
		
		tempS += (volume * 100.0f).ToString() + "%";
		
		if(Mathf.Approximately(.0f, mute))
			tempS += '\n' + "muted";
		
		volumeText = tempS;
	}
	
	public static void OnApplied()
	{
		/* Save current preferences here i suppose */
		
		AudioListener.volume = mute * volume;
		
		Screen.SetResolution(curRes.width, curRes.height, fullScreen);
		
		/* Reload scene to re-initialize buttons */
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
		
		Save();
	}
	
	public static void OnDeclined()
	{
		Load();
	}
	
	public static void OnDefault()
	{
		mute = 1.0f;
		volume = 1.0f;
		
		fullScreen = true;
		curRes = resolutionsList[0];
		OnApplied();
	}
	
	public static void OnMaxResolution()
	{
		fullScreen = true;
		curRes = fullScreenResolutions[fullScreenResolutions.Count() - 1];
		OnApplied();
	}
	
	public static void OnNextResolution()
	{
		++ curResIndex;
	}
	
	public static void OnPrevResolution()
	{
		-- curResIndex;
	}
	
	public static void OnToggleFullScreen()
	{
		fullScreen = !fullScreen;
	}
	
	public static void OnMoreVolume()
	{
		volume += volumeStep;
	}
	
	public static void OnLessVolume()
	{
		volume -= volumeStep;
	}
	
	public static void OnToggleMute()
	{
		if(Mathf.Approximately(.0f, mute))
			mute = 1.0f;
		else
			mute = .0f;
	}
	
	public static void OnBack()
	{
		SceneManager.LoadScene("Main_menu", LoadSceneMode.Single);
	}
}

[Serializable]
class Configs
{
	public bool fullScreen;
	public int resolutionWidth;
	public int resolutionHeight;
	public float mute;
	public float volume;
}
