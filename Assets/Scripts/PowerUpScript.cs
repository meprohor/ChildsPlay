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
            case 1: player.unlockTurn = true; break;
            case 2: player.unlockPush = true; break;
            case 3: player.unlockDoubleJump = true; break;
        }

        Destroy(gameObject);
    }
}
