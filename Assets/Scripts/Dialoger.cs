using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Text */
using UnityEngine.UI;

public class Dialoger : MonoBehaviour
{
	private GameObject playerGO;
	
    public Transform[] SpawnLocation;
    public GameObject[] ObjPrefab;
	
	private List<GameObject> ObjClone;
	
    // Use this for initialization

    void Activate()
    {
        SpawnSomething();
    }

    void SpawnSomething()
    {
		ObjClone = new List<GameObject>();
	
        ObjClone.Add(Instantiate(ObjPrefab[0], SpawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);
        ObjClone.Add(Instantiate(ObjPrefab[1], SpawnLocation[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);

        //ObjClone[1].AddComponent<FY>(); // Че, как это вообще использовать, че за хрень?
        ObjClone.Add(Instantiate(ObjPrefab[2], SpawnLocation[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);

		ObjClone[0].GetComponentInChildren<TextMesh>().text = "new text";
		
		playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
		if(null == ObjClone)
			return;
		
		ObjClone[0].transform.position = playerGO.transform.position;
    }

    void SpawnCubes()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<Rigidbody>();
                cube.transform.position = new Vector3(x / 2, y / 2, 0);
            }
        }
    }
}
