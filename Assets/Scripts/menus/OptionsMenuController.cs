// using System.Collections;
/* List */
using System.Collections.Generic;
/* MonoBehaviour */
using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
	private static OptionsMenuController _instance;
	public static OptionsMenuController instance
	{
		get { return _instance; }
		private set { _instance = value; }
	}
	
	[SerializeField] private float _volumeStep = .1f;
	public static float volumeStep
	{
		get { return instance._volumeStep; }
	}
	
	[SerializeField] private List<Vector2> _aspectRatios;
	private List<float> aspectRatiosList;
	public static List<float> aspectRatios
	{
		get
		{
			if(null == instance.aspectRatiosList)
			{
				if(null == instance._aspectRatios)
				{
					instance._aspectRatios = new List<Vector2>();
					Debug.LogError("No appliable Aspect Ratios found in Options Menu Controller");
					Debug.Break();
				}
				
				instance.aspectRatiosList = new List<float>();
				float ratio;
				
				foreach(Vector2 aspectRatioV2 in instance._aspectRatios)
				{
					ratio = Mathf.Round(100.0f * (aspectRatioV2.x / aspectRatioV2.y)) / 100.0f;
					
					instance.aspectRatiosList.Add(ratio);
				}
				
				instance._aspectRatios.Clear();
			}
			
			return instance.aspectRatiosList;
		}
	}
	
	[SerializeField] private Vector2 _minRes = new Vector2(.0f, .0f);
	private Resolution minResR = new Resolution();
	public static Resolution minRes
	{
		get
		{
			if(0 == instance.minResR.width
				|| 0 == instance.minResR.height)
			{
				if(.0f == instance._minRes.x
					|| .0f == instance._minRes.y)
				{
					Debug.Log("Invalid minimum resolution value");
					instance._minRes = new Vector2(800.0f, 600.0f);
				}
				
				instance.minResR.width = (int)instance._minRes.x;
				instance.minResR.height = (int)instance._minRes.y;
			}
			
			return instance.minResR;
		}
	}
	
	[SerializeField] private List<Vector2> _windowedResolutions;
	private List<Resolution> winResList;
	public static List<Resolution> windowedResolutions
	{
		get
		{
			if(null == instance.winResList)
			{
				if(null == instance._windowedResolutions)
				{
					instance._windowedResolutions = new List<Vector2>();
					Debug.LogError("No windowed resolutions found in Options Menu Controller");
					Debug.Break();
				}
				
				instance.winResList = new List<Resolution>();
				Resolution tempR = new Resolution();
				
				foreach(Vector2 resolutionV2 in instance._windowedResolutions)
				{
					Resolution maxRes = Screen.resolutions[Screen.resolutions.Length - 1];
					if((int)resolutionV2.x > maxRes.width
						|| (int)resolutionV2.y > maxRes.height)
						continue;
					
					tempR.width = (int)resolutionV2.x;
					tempR.height = (int)resolutionV2.y;
					
					instance.winResList.Add(tempR);
				}
				
				instance._windowedResolutions.Clear();
			}
			
			return instance.winResList;
		}
	}
	
	void Awake()
	{
		instance = this;
		OptionsMenu.gameObject.SetActive(true);
		
		OptionsMenu.ManageResolutionText();
		OptionsMenu.ManageVolumeText();
		
		OptionsMenu.Load();
	}
}
