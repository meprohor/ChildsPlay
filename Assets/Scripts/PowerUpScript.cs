using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
    public GameObject affected;
    private PlayerScript player;

    [HideInInspector]
    public int choiceIndex = 0;

    void Start()
    {
          player = affected.GetComponent<PlayerScript>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        switch (choiceIndex)
        {
            case 0: player.unlockJump = true; break;
            case 1: player.unlockLeft = true; break;
            case 2: player.unlockKick = true; break;
        }

        Destroy(gameObject);
    }
}
