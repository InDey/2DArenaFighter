using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public bool facingRight = true;			// For determining which way the player is currently facing.

	public bool jump = false;				// Condition for whether the player should jump.
	public int jumpCount = 0;				// Initial start to allow for multiple jumps
	public int maxJumps = 2;				// the max jumps a player can do
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 6000f;			// Amount of force added when the player jumps.

	public bool dodge = false;				// Condition for whether the player should dodge
	public float dodgeDist = 10f;			// The distance one can dodge
	public bool canDodge = false;
	public float DodgeDelay = 1f;
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 50f;				// The fastest the player can travel in the x axis.


	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.
	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.

	public bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.


	void Awake ()
	{
		// Get animator
//		anim = GetComponent<Animator> (); 
	}

	public void inputs ()
	{
		// If the jump button is pressed and the player is grounded then the player should jump.
		if (Input.GetButtonDown ("Jump") && jumpCount < maxJumps) {
			jump = true;
			jumpCount += 1;
		}
		
		// If the Dodge button "f" is pressed then the player can dodge
		if (Input.GetButtonDown ("Dodge") && (canDodge || grounded)) {
			dodge = true;
		}
	}

	public void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public IEnumerator Taunt ()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range (0f, 100f);
		if (tauntChance > tauntProbability) {
			yield return new WaitForSeconds (tauntDelay);

			if (!audio.isPlaying) {
				tauntIndex = TauntRandom ();
				audio.clip = taunts [tauntIndex];
				audio.Play ();
			}
		}
	}

	public void PlayerMovement ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		
		// stop the playing from spinning
		//TODO fix this when touching uneven ground
		if (!grounded)
			transform.eulerAngles = new Vector3 (0, 0, 0);
		
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//		anim.SetFloat("Speed", Mathf.Abs(h));
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce (Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !facingRight)
			Flip ();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight)
			Flip ();
		
		// If the player should jump...
		if (jump && jumpCount <= maxJumps) {
			// Set the Jump animator trigger parameter.
			//			anim.SetTrigger ("Jump");
			
			// Play a random jump audio clip.
			int i = Random.Range (0, jumpClips.Length);
			//			AudioSource.PlayClipAtPoint (jumpClips [i], transform.position);
			
			rigidbody2D.AddForce (new Vector2 (0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied. 
			jump = false;
		} // end jump
		
		// If the player Should Dodge...
		if (dodge) {
			dodge = false;
			canDodge = false;
			transform.position = new Vector3 (transform.position.x + (h * dodgeDist), transform.position.y + (v * dodgeDist), 0);
			
		}
	}

	// is this even necessary for us yet? I CAN BARELY DO NORMAL ANIMATIONS!!!!
	int TauntRandom ()
	{
		int i = Random.Range (0, taunts.Length);
		if (i == tauntIndex)
			return TauntRandom ();
		else
			return i;
	}

	// Collision Listeners
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "Platform") {
			grounded = true;
			jumpCount = 0;
			canDodge = true;
		}
	}

	void OnCollisionExit2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "Platform")
			grounded = false;
		
	}

	// Getters and Setters
	public void setSpeed (float speed)
	{
		maxSpeed = speed;
	}

	public void setJump (int numJumps, float jumpStr)
	{
		maxJumps = numJumps;
		jumpForce = jumpStr;
	}

	public void setCharacter (float speed, int numJumps, float jumpStr, float distDodge)
	{
		maxSpeed = speed;
		maxJumps = numJumps;
		jumpForce = jumpStr;
		dodgeDist = distDodge;
	}
}

public class Pirate : baseCharacter
{
	public Pirate ()
	{
		speed = 40;
		jumps = 3;
		jumpForce = 4000;
		dodgeDist = 15;
	}
}

public class Ninja : baseCharacter
{
	public Ninja ()
	{
		speed = 50;
		jumps = 4;
		jumpForce = 3500;
		dodgeDist = 20;
	}
}

public class Knight : baseCharacter
{
	public Knight ()
	{
		speed = 30;
		jumps = 1;
		jumpForce = 7500;
		dodgeDist = 5;
	}
}

public class Wizard : baseCharacter
{
	public Wizard ()
	{
		speed = 40;
		jumps = 2;
		jumpForce = 4500;
		dodgeDist = 30;
	}
}

public class baseCharacter
{
	protected float speed;
	protected int jumps;
	protected float jumpForce;
	protected float dodgeDist;

	public float getSpeed ()
	{
		return speed;
	}

	public int getNumJumps ()
	{
		return jumps;
	}

	public float getJumpForce ()
	{
		return jumpForce;
	}

	public float getDodgeDist ()
	{
		return dodgeDist;
	}
}
