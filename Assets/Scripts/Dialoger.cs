using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Text */
using UnityEngine.UI;

public class Dialoger : MonoBehaviour
{
    // Для создания объекта (ПОЛЯ СКРИПТА)
    public List<Transform> SpawnLocation;
    public List<GameObject> ObjPrefab;
    public List<string> Message;
    public List<int> TimeToDestroy;
    public List<bool> IsPlayerParent;

    /*public class OneMessage
    {
        public Transform SpawnLocation;
        public GameObject ObjPrefab;
        public string Message;
        public int TimeToDestroy;
        public bool IsPlayerParent;
    }

    public List<OneMessage> InputObj;*/

    // Ссылка на объект игрока (Чтобы к нему крепить сообщения)
    private GameObject playerGO;
    // Чтобы знать какое сообщения мы показывали в последний раз
    private int CurrentShowMessage = -1;
    // Объекты, которые будем выводить и прочее
    private List<GameObject> ObjClone = new List<GameObject>();
	

    void Start () {
        // Находим ОБЪЕКТ игрока чтобы к нему можно было прицепить текст
        playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
        // Переносы текста
        for (int i = 0; i < Message.Count;i++ )
        {
            if (Message[i].Length > 14) Message[i]=Message[i].Insert(14, "\n");
        }
	}

    void Activate()
    {
        ShowNextMessage();
    }
    void ShowNextMessage()
    {
        // Если показаны все прописанные фразу - ничего не выводить
        if (CurrentShowMessage < ObjPrefab.Count - 1) CurrentShowMessage++; else return;
        // Добавляем инфу о новом сообщении
        ObjClone.Add(Instantiate(ObjPrefab[CurrentShowMessage], SpawnLocation[CurrentShowMessage].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);
        // Устанавливаем новый текст объекту
        ObjClone[CurrentShowMessage].GetComponentInChildren<TextMesh>().text = Message[CurrentShowMessage]; 
    }

    /*void SpawnSomething()
    {
		ObjClone = new List<GameObject>();

        ObjClone.Add(Instantiate(ObjPrefab[0], SpawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);
        ObjClone.Add(Instantiate(ObjPrefab[1], SpawnLocation[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject);

        ObjClone[0].GetComponentInChildren<TextMesh>().text = Message[0];
		
		playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
    }*/

    // Update is called once per frame
    void Update()
    {
        // Бежим по всем объектам и приближаем их к смерти(!)
        for (int i = 0; i < ObjClone.Count; i++)
            if (TimeToDestroy[i] > 0)
            {
                // ДЕинкрементируем время то самоуничтожения
                TimeToDestroy[i] -= 1;
                // Следует за игроком только если это необходимо
                if (IsPlayerParent[i]) ObjClone[i].transform.position = playerGO.transform.position + new Vector3(-6, 7 + Mathf.Sin(TimeToDestroy[i] / (4*Mathf.PI))/2, 0);
            }
            else
                Destroy(ObjClone[i]);
        //ObjClone[0].GetComponentInChildren<TextMesh>().text = Message[0] + TimeToDestroy[0].ToString(); // DEBUG
    }
}
