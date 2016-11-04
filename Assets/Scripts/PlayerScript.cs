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
    private Animator animatorComponent;

    // Bool variable to check if the object is standing on the ground and can jump
    //[HideInInspector]
    public bool isGrounded = true;
    private GameObject groundedOn = null;

    // false - t-roller facing right
    // true - t-roller facing left
    private bool turn = false;

    private GameObject soundEffectsHelper;

    private float timeBetweenTurns = 0.3333f;
	private float timestamp;

    private bool isGameOver = false;

    // Make a falling sound when player hits the ground
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform") && !isGrounded){
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.point.y < transform.position.y){
                    soundEffectsHelper.SendMessage("FallingSound");
                }
            }
        }
    }

    // Check if player's standing on the ground
    void OnCollisionStay2D(Collision2D other)
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

    // Check if player left the ground
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject == groundedOn){
            isGrounded = false;
            groundedOn = null;
        }
    }

    // Iniatialize variables
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();

        animatorComponent = GetComponentInChildren<Animator>();

        soundEffectsHelper = GameObject.Find("soundEffectsHelper");
    }

    // Jump up with velocity spd
    void Jump(float spd)
    {
        // Make a jumping sound
        soundEffectsHelper.SendMessage("JumpingSound");
    	
        // Set jump speed 
        Vector2 v = rigidbodyComponent.velocity;
    	v.y = spd;
    	rigidbodyComponent.velocity = v;
    }

    // Set variable isGameOver to block all input and trigger death animation
    void GameOver()
    {
        isGameOver = true;
        animatorComponent.SetTrigger("GameOver");
    }

    // Handle user input
    void Update()
    {
        // If the game is over block all input
        if(!isGameOver){
            // Retrieve axis information
            float inputX = 0;

            if(Input.GetButton("Vertical"))
                inputX = Input.GetAxis("Vertical");

            // We only allow 3 button presses in one second
            if(Time.time >= timestamp && Input.GetButton("Turn")){
                turn = !turn;
                timestamp = Time.time + timeBetweenTurns;
            }

       	    // Turn player
            Vector3 temp = transform.localScale;
            if(turn){
                inputX = -1*Mathf.Abs(inputX);
                if(transform.localScale.x > 0){
                    temp.x *= -1;
                    transform.localScale = temp;
                }
            } else {
                inputX = Mathf.Abs(inputX);
                if(transform.localScale.x < 0){
                    temp.x *= -1;
                    transform.localScale = temp;
                }
            }

            // Set the animator variable to trigger moving animation
            animatorComponent.SetFloat("Speed", Mathf.Abs(rigidbodyComponent.velocity.x)/15.0f);

            // Set the X component for the movement vector
            movement = new Vector2(
             speed.x * inputX,
             rigidbodyComponent.velocity.y);


            // Make a jump
            if (unlockJump && Input.GetButton("Jump") && isGrounded)
               Jump(speed.y);
        } else {
            movement = Vector3.zero;
        }
    }


    void FixedUpdate()
    {
        // Move the game object
        rigidbodyComponent.velocity = movement;
    }

}
