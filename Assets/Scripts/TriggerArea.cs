using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour {

    /* Game objects affected by enter this TriggerArea*/
    public GameObject[] affected;

    // Если true - то больше не будет работать
    public bool isTriggerOnce = true;
    public bool isActivated = false;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            GameObject.FindWithTag("GameOverMenu").SendMessage("GameOver");
        }*/
        if (isActivated && isTriggerOnce) return;
        /* Check if it is player who's touching TriggerArea */
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            // Активируем всех, кто привязан к триггеру
            foreach (GameObject affectedObject in affected)
            {
                affectedObject.SendMessage("Activate");
            }
            isActivated = true;
        }
    }
}
