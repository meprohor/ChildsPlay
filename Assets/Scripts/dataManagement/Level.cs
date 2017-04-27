using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	void Awake()
	{
		string tempS = SceneManager.GetActiveScene().name;
		
		if(!Game.unlockedLevels.Contains(tempS))
			Game.unlockedLevels.Add(tempS);
		
		Game.Save();
	}
}
