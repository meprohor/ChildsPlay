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

    // GameObject which plays sound effects
    private GameObject soundEffectsHelper;

    // Stop player from smashing button too fast
    private float timeBetweenPresses = 0.3333f;
	private float timestampTurns;

    // TimeStamp for playing idle animation
    public float timeUntilIdle = 1.0f;
    private float idleStamp;

    // For blocking user input when the game is over
    private bool isGameOver = false;

    // For allowing player to double jump
    private bool doubleJump = true;

    // For determining if the player pressed jump during this frame
    private bool jumped = false;

    // For dynamic changes to order in layer
    private SpriteRenderer[] spriteRenderers;
    private int[] orderOffset;

    private float walkAnimationSwitch = -1;

    // Make a falling sound when player hits the ground
    void OnCollisionEnter2D(Collision2D other)
    {
        if((other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Box")) && !isGrounded){
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.point.y < transform.position.y){
                    soundEffectsHelper.SendMessage("FallingSound");
                    animatorComponent.SetBool("Jump", false);
                }
            }
        }
    }

    // Check if player's standing on the ground
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Box")){
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

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        int i=0;
        orderOffset = new int[spriteRenderers.Length];
        foreach( SpriteRenderer sprite in spriteRenderers ){
            if(sprite != null){
                orderOffset[i] = sprite.sortingOrder;
               i++;
            }
        }


        idleStamp = timeUntilIdle;
    }

    // Jump up with velocity spd
    void Jump(float spd)
    {
        animatorComponent.SetBool("Jump", true);

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
        animatorComponent.SetBool("Jump", false);
        animatorComponent.SetTrigger("GameOver");
    }

    // Handle user input
    void Update()
    {
        // If the game is over block all input
        if(!isGameOver && !PauseMenuScript.isOnPause){
            if(Time.time >= idleStamp){
                animatorComponent.SetBool("Idle", true);
            }

            if(Input.anyKey){
                idleStamp = Time.time + timeUntilIdle;
                animatorComponent.SetBool("Idle", false);
            }

            // Retrieve axis information
            float inputX = 0;

            if(Input.GetButton("Vertical")){
                inputX = Input.GetAxis("Vertical");
                if(walkAnimationSwitch == -1){
                    walkAnimationSwitch = Time.time;
                } else {
                    walkAnimationSwitch = Time.time - walkAnimationSwitch;
                    if(walkAnimationSwitch > 3.0f)
                        animatorComponent.SetBool("Run", true);
                }
            } else {
                walkAnimationSwitch = -1;
                animatorComponent.SetBool("Run", false);
            }

            // We only allow 3 button presses in one second
            if(Time.time >= timestampTurns && Input.GetButton("Turn") && unlockTurn){
                turn = !turn;
                timestampTurns = Time.time + timeBetweenPresses;
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


            // Jump or double jump
            if(unlockJump && Input.GetKeyDown(KeyCode.Space)){
                if(isGrounded || (doubleJump && unlockDoubleJump)){
                    jumped = true;
                    if(!isGrounded && doubleJump){
                        doubleJump = false;
                    }
                }
            }
        }
        if(isGameOver)
                movement = Vector3.zero;
    }


    void FixedUpdate()
    {
        // Move the game object
        rigidbodyComponent.velocity = movement;

        // If player pressed jump during frame update
        // Set y velocity
        if(jumped){
            Jump(speed.y);
            jumped = false;
        }

        // If player is grounded
        // Recharge double jump
        if(isGrounded)
            doubleJump = true;
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject.CompareTag("Door"))
    		if(other.transform.position.x > transform.position.x){
    			foreach( SpriteRenderer sprite in spriteRenderers )
    				sprite.sortingOrder += 10;
    		} else {
    			int i = 0;
    			foreach( SpriteRenderer sprite in spriteRenderers )
    				sprite.sortingOrder = orderOffset[i++];
    		}
    }
}
