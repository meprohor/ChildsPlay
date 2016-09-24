using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    // 2 - Store the movement and the component
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private bool canJump = false;
    private SpriteRenderer spriterendererComponent;

    void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        canJump = false;
    }

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        spriterendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 3 - Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");

        movement = new Vector2(
          speed.x * inputX,
          rigidbodyComponent.velocity.y);
        if(inputX > 0)
        {
            spriterendererComponent.flipX = true;
        }

        if (inputX < 0)
        {
            spriterendererComponent.flipX = false;
        }

        if (Input.GetKeyDown("space") && canJump)
        {
            movement.y = speed.y;
        }
    }

    void FixedUpdate()
    {
        // 6 - Move the game object
        rigidbodyComponent.velocity = movement;
    }
}
