using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialoger : MonoBehaviour
{
    public Transform[] SpawnLocation;
    public GameObject[] ObjPrefab;
    public GameObject[] ObjClone;
    // Use this for initialization

    void Start()
    {
        SpawnSomething();
    }

    void SpawnSomething()
    {
        ObjClone[0] = Instantiate(ObjPrefab[0], SpawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //ObjClone[0].GetComponent<TextMesh>().text = "new text";
        ObjClone[1] = Instantiate(ObjPrefab[1], SpawnLocation[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

        //ObjClone[1].AddComponent<FY>(); // Че, как это вообще использовать, че за хрень?
        ObjClone[2] = Instantiate(ObjPrefab[2], SpawnLocation[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {

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
