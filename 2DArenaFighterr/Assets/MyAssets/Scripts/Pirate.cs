using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pirate : baseCharacter
{
    public Pirate()
    {
        speed = 40;
        jumps = 3;
        jumpForce = 4000;
        dodgeDist = 15;
    }
}
