using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Knight : baseCharacter
{
    public Knight()
    {
        speed = 30;
        jumps = 1;
        jumpForce = 7500;
        dodgeDist = 5;
    }
}
