using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public static class Game
{
	public static string dataPath
	{
		get { return Application.persistentDataPath + "sav.dat"; }
	}
	
	public static string levelName;
	public static List<string> _unlockedLevels;
	public static List<string> unlockedLevels
	{
		set { _unlockedLevels = value; }
		get
		{
			if(null == _unlockedLevels)
				_unlockedLevels = new List<string>();
			
			return _unlockedLevels;
		}
	}
	
	public static void Save()
	{
		BinaryFormatter tempBF = new BinaryFormatter();
		FileStream tempFS = File.Create(dataPath);
		
		GameData data = new GameData();
		data.levelName = SceneManager.GetActiveScene().name;
		data.unlockedLevels = new List<string>(unlockedLevels);
		
		tempBF.Serialize(tempFS, data);
		tempFS.Close();
	}
	
	public static void Load()
	{
		if(!File.Exists(dataPath))
			return;
		
		BinaryFormatter tempBF = new BinaryFormatter();
		FileStream tempFS = File.Open(dataPath, FileMode.Open);
		
		GameData data = (GameData)tempBF.Deserialize(tempFS);
		tempFS.Close();
		
		levelName = data.levelName;
		unlockedLevels = new List<string>(data.unlockedLevels);
	}
}

[Serializable]
class GameData
{
	public string levelName;
	public List<string> unlockedLevels;
}