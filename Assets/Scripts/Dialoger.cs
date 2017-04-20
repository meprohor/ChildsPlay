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
    public string[] Message;
    private int[] TimeToDestroy;
	
	private List<GameObject> ObjClone;
	
    void Start () {

	}

    void Activate()
    {
        SpawnSomethingTest();
    }
    void SpawnSomethingTest()
    {
        ObjClone = new List<GameObject>();
        ObjClone.Add(Instantiate(ObjPrefab[0], SpawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);
        ObjClone[ObjClone.Count - 1].GetComponentInChildren<TextMesh>().text = Message[ObjClone.Count - 1];
        //TimeToDestroy[ObjClone.Count - 1] = 600;
        playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    void SpawnSomething()
    {
		ObjClone = new List<GameObject>();

        ObjClone.Add(Instantiate(ObjPrefab[0], SpawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);
        ObjClone.Add(Instantiate(ObjPrefab[1], SpawnLocation[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);

		ObjClone[0].GetComponentInChildren<TextMesh>().text = "new text";
		
		playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
		if(null == ObjClone)
			return;
        ObjClone[0].transform.position = playerGO.transform.position;
        /*if (TimeToDestroy[0] > 0) { TimeToDestroy[0] -= 1; ObjClone[0].transform.position = playerGO.transform.position; }
        else Destroy(ObjClone[0]);*/
    }
}
