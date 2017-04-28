using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	private static MainMenuController _instance;
	public static MainMenuController instance
	{
		get { return _instance; }
		private set { _instance = value; }
	}
	
	public string _firstLevel = "";
	public static string firstLevel
	{
		get { return instance._firstLevel; }
	}
	
	void Awake()
	{
		Game.Load();
		
		instance = this;
		MainMenu.gameObject.SetActive(true);
		MainMenu.OnPrevLevel();
	}
}
