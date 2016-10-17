using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScriptBETA : MonoBehaviour
{
    /// <summary>
    /// Moving Speed of the character
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    // Movement Vector
    private Vector2 movement;

    // Store RigidBody component
    private Rigidbody2D rigidbodyComponent;

    // Store SpriteRenderer component
    // Bool variable to check if the object is standing on the ground and can jump
   // [HideInInspector]
    public bool canJump = true;

    // Variables for unlocking abilities
    public bool unlockJump;
    public bool unlockLeft;
    public bool unlockKick;

    // GameObjects for body and wheel
    private GameObject body;
    private GameObject wheel;

    // Their spriterenderer's 
    private SpriteRenderer spriteRendererWheel;
    private SpriteRenderer spriteRendererBody;

    // Their rigidBodies
    private Rigidbody2D rigidbodyWheel;
    private Rigidbody2D rigidbodyBody;


    void OnCollisionEnter2D(Collision2D other)
    {
        foreach (ContactPoint2D contact in other.contacts)
        {
            if (contact.point.y < transform.position.y){
                canJump = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        canJump = false;
    }

    void Start()
    {
        // Get gameobjects
        wheel = transform.FindChild( "wheel" ).gameObject;
        body = transform.FindChild( "body" ).gameObject;

        // Get their rigidBodies
        rigidbodyWheel = wheel.GetComponent<Rigidbody2D>();
        rigidbodyBody = body.GetComponent<Rigidbody2D>();
        rigidbodyBody.centerOfMass = new Vector3(0.5f, 0, 0);

        // Get their spriterenderer's
        spriteRendererWheel = wheel.GetComponent<SpriteRenderer>();
        spriteRendererBody = body.GetComponent<SpriteRenderer>();

        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        
        if(!unlockLeft && inputX < 0)
        {
            inputX *= -1;
        }

        // Set the X component for the movement vector
        movement = new Vector2(
          speed.x * inputX,
          rigidbodyComponent.velocity.y);

        // Check where our hero is facing
        if(inputX > 0)
        {
            spriteRendererWheel.flipX = false;
            spriteRendererBody.flipX = false;
        }

        if (inputX < 0)
        {
            spriteRendererWheel.flipX = true;
            spriteRendererBody.flipX = true;
        }


        // Set the y component of the movement vector
        if (unlockJump && Input.GetButton("Jump") && canJump)
        {
            movement.y = speed.y;
        }
    }


    void FixedUpdate()
    {
        // Move the game object
        // Put the wheel and the body to where their parent object is
        body.transform.position = transform.position;
        wheel.transform.position = new Vector3(transform.position.x+0.29f, transform.position.y-1.46f, transform.position.z);

        // Set the speed vector for player
        rigidbodyComponent.velocity = movement;

        // Rotate the wheel and player model
        rigidbodyBody.MoveRotation(movement.x);

        rigidbodyWheel.MoveRotation(movement.x+rigidbodyWheel.rotation);
    }



}
