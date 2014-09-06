using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class baseCharacter
{
    protected float speed;
    protected int jumps;
    protected float jumpForce;
    protected float dodgeDist;

    public float getSpeed()
    {
        return speed;
    }

    public int getNumJumps()
    {
        return jumps;
    }

    public float getJumpForce()
    {
        return jumpForce;
    }

    public float getDodgeDist()
    {
        return dodgeDist;
    }
}
