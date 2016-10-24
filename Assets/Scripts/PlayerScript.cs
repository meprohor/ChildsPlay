using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// Moving Speed of the character
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    // Variables for unlocking abilities
    public bool unlockJump;
    public bool unlockTurn;
    public bool unlockPush;
    public bool unlockDoubleJump;

    // Movement Vector
    private Vector2 movement;

    // Store RigidBody component
    private Rigidbody2D rigidbodyComponent;

    // Store SpriteRenderer component
    private SpriteRenderer spriterendererComponent;

    // Bool variable to check if the object is standing on the ground and can jump
    //[HideInInspector]
    public bool isGrounded = true;
    private GameObject groundedOn = null;

    // false - t-roller facing right
    // true - t-roller facing left
    private bool turn = false;

    private float timeBetweenTurns = 0.3333f;
	private float timestamp;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform")){
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.point.y < transform.position.y){
                    isGrounded = true;
                    groundedOn = other.gameObject;
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject == groundedOn){
            isGrounded = false;
            groundedOn = null;
        }
    }

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();

        spriterendererComponent = GetComponent<SpriteRenderer>();
    }

    void Jump(float spd){
    	Vector2 v = rigidbodyComponent.velocity;
    	v.y = spd;
    	rigidbodyComponent.velocity = v;
    }

    void Update()
    {
        // Retrieve axis information
        float inputX = 0;
        if(Input.GetButton("Vertical"))
        	 inputX = Input.GetAxis("Vertical");
        
       	if(Time.time >= timestamp && Input.GetButton("Turn")){
       		turn = !turn;
       		timestamp = Time.time + timeBetweenTurns;
       	}

        if(turn){
            inputX *= -1;
            spriterendererComponent.flipX = true;
        } 
     	else{
     		spriterendererComponent.flipX = false;
     	}


        // Set the X component for the movement vector
        movement = new Vector2(
          speed.x * inputX,
          rigidbodyComponent.velocity.y);


        // Set the y component of the movement vector
        if (unlockJump && Input.GetButton("Jump") && isGrounded)
           Jump(speed.y);
    }


    void FixedUpdate()
    {
        // Move the game object
        rigidbodyComponent.velocity = movement;

        rigidbodyComponent.MoveRotation(movement.x);
    }

}
