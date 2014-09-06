using UnityEngine;

public class CharacterKnight : Character
{
	public CharacterKnight()
    {
        speed = 30;
		jumpCount = 1;
		jumpForce = 7500;
		dodgeDist = 5;
		maxJumps = 1;
    }

    
	public override void Shoot(float h, float v)
	{
		// If the player should shoot...
		if (shoot)
		{
			shoot = false;
			canShoot = false;
			
			GameObject obj = ObjectPooler.current.GetPooledObject();
			
			if (obj != null)
			{
				obj.transform.position = transform.position;
				obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
				obj.SetActive(true);
				
				if (facingRight)
				{
					obj.transform.rotation = Quaternion.Euler(new Vector3(180f, 0, 0));
					obj.rigidbody2D.velocity = new Vector2(starSpeed, 0);
				}
				else
				{
					obj.transform.position = new Vector3(transform.position.x - 1f, transform.position.y, 0);
					obj.rigidbody2D.velocity = new Vector2(-starSpeed, 0);
				}
			}
		}
	}
}
