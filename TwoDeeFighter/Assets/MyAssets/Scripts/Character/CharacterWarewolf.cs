using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CharacterWarewolf : Character
{
	public CharacterWarewolf()
    {
		characterType = WAREWOLF;
        speed = 50;
        jumpCount = 4;
        jumpForce = 3500;
        dodgeDist = 20;
    }
}
