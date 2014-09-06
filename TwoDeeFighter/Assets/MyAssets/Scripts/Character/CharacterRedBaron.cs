using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Sprites;

public class CharacterRedBaron : Character
{
	public CharacterRedBaron()
    {
		characterType = RED_BARON;
        speed = 40;
        jumpCount = 3;
        jumpForce = 4000;
        dodgeDist = 15;
    }
}
