using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CharacterRobot : Character
{
	public CharacterRobot()
    {
        speed = 40;
        jumpCount = 2;
        jumpForce = 4500;
        dodgeDist = 30;
    }
}
