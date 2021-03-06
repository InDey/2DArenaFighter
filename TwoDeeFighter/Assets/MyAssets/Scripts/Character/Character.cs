using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Character : MonoBehaviour
{
    public enum CharacterType
    {
        WAREWOLF,
        KNIGHT,
        RED_BARON,
        ROBOT
    };

    public static readonly String TAG_JUMP = "KeyO";
    public static readonly String TAG_DODGE = "KeyA";
    public static readonly String TAG_MOVE1 = "KeyU";
    public static readonly String TAG_MOVE2 = "KeyY";
    public static readonly String TAG_GROUND = "ground";
    public static readonly String TAG_PLATFORM = "Platform";
    public static readonly String TAG_WALL = "Wall";

    public bool facingRight = true;			// For determining which way the player is currently facing.
    public bool grounded = false;		    // Whether or not the player is grounded.
    protected float speed;

    // Scoring
    public int lives = 5;
    public bool hasLives = true;
    public int kills = 0;
    public int points = 0;

    //JUMP
    public bool jump = false;		        // Condition for whether the player should jump.
    public int jumpCount = 0;			    // Initial start to allow for multiple jumps
    public int maxJumps = 2;			    // the max jumps a player can do
    public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    public float jumpForce = 6000f;		    // Amount of force added when the player jumps.

    //DODGE
    public bool dodge = false;		        // Condition for whether the player should dodge
    public float dodgeDist = 10f;			// The distance one can dodge
    public bool canDodge = false;
    public float DodgeDelay = 1f;
    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 50f;			// The fastest the player can travel in the x axis.

    //SHOOT
    public bool canShoot = false;		    // Condition for whether the player should shoot
    public bool shoot = false;		        // When true, the player shoots
    public float starSpeed = 2f;			// NinjaStar's thrown velocity

    public AudioClip[] taunts;				// Array of clips for when the player taunts.
    public float tauntProbability = 50f;	// Chance of a taunt happening.
    public float tauntDelay = 1f;			// Delay for when the taunt should happen.
    private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.

    private Animator anim;					// Reference to the player's animator component.

    void Awake()
    {
        // Get animator
        //		anim = GetComponent<Animator> (); 
    }

    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void Inputs()
    {
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown(TAG_JUMP) && jumpCount < maxJumps)
        {
            jump = true;
            jumpCount += 1;
        }

        // If the Dodge button "f" is pressed then the player can dodge
        if (Input.GetButtonDown(TAG_DODGE) && (canDodge || grounded))
        {
            dodge = true;
        }

        // If move 1 is pressed and canMove1 is ready
        if (Input.GetButtonDown(TAG_MOVE1))
        {
            shoot = true;
        }

        // If move 1 is pressed and canMove1 is ready
        if (Input.GetButtonDown(TAG_MOVE2))
        {

        }
    }

    public void PlayerMovement()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // stop the playing from spinning
        //TODO fix this when touching uneven ground
        if (!grounded)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //		anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * rigidbody2D.velocity.x < maxSpeed)
        {
            rigidbody2D.AddForce(Vector2.right * h * moveForce);
        }

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        // Jump
        if (jump && jumpCount <= maxJumps)
        {
            Jump(h, v);
        }

        // Dodge
        if (dodge)
        {
            Dodge(h, -v);
        }

        // Shoot
        if (shoot)
        {
            Shoot(h, v);
        }
    }


    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Jump(float h, float v)
    {
        // Set the Jump animator trigger parameter.
        //			anim.SetTrigger ("Jump");

        // Play a random jump audio clip.
        //int i = Random.Range(0, jumpClips.Length);
        //			AudioSource.PlayClipAtPoint (jumpClips [i], transform.position);

        rigidbody2D.AddForce(new Vector2(0f, jumpForce));

        // Make sure the player can't jump again until the jump conditions from Update are satisfied. 
        jump = false;
    }

    public void Dodge(float h, float v)
    {
        dodge = false;
        canDodge = false;
        transform.position = new Vector3(transform.position.x + (h * dodgeDist), transform.position.y + (v * dodgeDist), 0);
    }

    public virtual void Shoot(float h, float v)
    {

    }

    public void setMaxSpeed(float maxSpeed)
    {
        this.maxSpeed = maxSpeed;
    }

    public void setJump(int maxJumps, float jumpForce)
    {
        this.maxJumps = maxJumps;
        this.jumpForce = jumpForce;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getNumJumps()
    {
        return jumpCount;
    }

    public float getJumpForce()
    {
        return jumpForce;
    }

    public float getDodgeDist()
    {
        return dodgeDist;
    }

    public static Character getCharacter(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.WAREWOLF:
                {
                    CharacterWarewolf warewolf = new CharacterWarewolf();
                    //TODO set any initial things here
                    return warewolf;
                }
            case CharacterType.KNIGHT:
                {
                    CharacterKnight knight = new CharacterKnight();
                    return knight;
                }
            case CharacterType.RED_BARON:
                {
                    CharacterRedBaron redbaron = new CharacterRedBaron();
                    return redbaron;
                }
            case CharacterType.ROBOT:
                {
                    CharacterRobot robot = new CharacterRobot();
                    return robot;
                }
        }
        return null;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == TAG_GROUND || coll.gameObject.tag == TAG_PLATFORM || coll.gameObject.tag == TAG_WALL)
        {
            grounded = true;
            jumpCount = 0;
            canDodge = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == TAG_GROUND || coll.gameObject.tag == TAG_PLATFORM)
        {
            grounded = false;
        }
    }
}



