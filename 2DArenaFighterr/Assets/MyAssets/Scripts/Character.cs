using UnityEngine;
using System.Collections;

public class Character : PlayerControl
{


	public enum characterEnum
	{
		PRT = 1,
		NJA = 2,
		KNT = 3,
		WIZ = 4
	}
	;
	public characterEnum charChoice;
	private characterEnum currentChar;


	// Use this for initialization
	void Start ()
	{
	
	}

	void Awake ()
	{
		chooseCharacter (charChoice);
	}
	
	// Update is called once per frame
	void Update ()
	{
		inputs ();
	}

	void FixedUpdate ()
	{
		PlayerMovement ();

		if (currentChar != charChoice)
			chooseCharacter (charChoice);
	}

	public void chooseCharacter (characterEnum Choice)
	{
		switch (Choice) {
		case characterEnum.PRT:
			{
				Pirate Prt = new Pirate ();
				setCharacter (Prt.getSpeed (), Prt.getNumJumps (), Prt.getJumpForce (), Prt.getDodgeDist ());
				setChoice (characterEnum.PRT);
				break;
			}
		case characterEnum.NJA:
			{
				Ninja Nja = new Ninja ();
				setCharacter (Nja.getSpeed (), Nja.getNumJumps (), Nja.getJumpForce (), Nja.getDodgeDist ());
				setChoice (characterEnum.NJA);
				break;
			}
		case characterEnum.KNT:
			{
				Knight knt = new Knight ();
				setCharacter (knt.getSpeed (), knt.getNumJumps (), knt.getJumpForce (), knt.getDodgeDist ());
				setChoice (characterEnum.KNT);
				break;
			}
		case characterEnum.WIZ:
			{
				Wizard wiz = new Wizard ();
				setCharacter (wiz.getSpeed (), wiz.getNumJumps (), wiz.getJumpForce (), wiz.getDodgeDist ());
				setChoice (characterEnum.WIZ);
				break;
			}
		default:
			{
				chooseCharacter ((characterEnum)Random.Range (1, 4));
				break;	
			}
		} // end switch
	}
	
	public void setChoice (characterEnum Choice)
	{
		charChoice = Choice;
		currentChar = Choice;
	}

}



