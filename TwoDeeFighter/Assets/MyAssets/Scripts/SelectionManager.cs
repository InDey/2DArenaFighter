using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour
{

    public const string P1 = "P1";
    public const string P2 = "P2";
    public const string P3 = "P3";
    public const string P4 = "P4";

    public const string VERT = "Vertical";
    public const string HORIZ = "Horizontal";
    public const string KEY_O = "KeyO";
    public const string KEY_U = "KeyU";
    public const string KEY_Y = "KeyY";
    public const string KEY_A = "KeyA";

    public const int NUM_CHARS = 4;
    public int num_Players_selected = 0;

    public Transform[] P1Pos = new Transform[NUM_CHARS];
    public Transform[] P2Pos = new Transform[NUM_CHARS];
    public Transform[] P3Pos = new Transform[NUM_CHARS];
    public Transform[] P4Pos = new Transform[NUM_CHARS];

    public static bool p1CanMove = true;
    public static bool p2CanMove = true;
    public static bool p3CanMove = true;
    public static bool p4CanMove = true;

    public class playerChoice
    {
        public GameObject icon;
        public int index = -1;
        public GameObject finalChoice;
    }

    public playerChoice P1choice;
    public playerChoice P2choice;
    public playerChoice P3choice;
    public playerChoice P4choice;

    public GameObject Warewolf;
    public GameObject Knight;
    public GameObject Red_Baron;
    public GameObject Robot;

    void Awake()
    {
        num_Players_selected = 0;
        p1CanMove = true;
        p2CanMove = true;
        p3CanMove = true;
        p4CanMove = true;

        //TODO make these start at the starting position until you move

        // P1 setup
        P1choice = new playerChoice();
        P1choice.finalChoice = null;
        P1choice.icon = GameObject.FindGameObjectWithTag("P1");
		if (P1choice.index >= 0) {
			P1choice.icon.transform.position = P1Pos[P1choice.index].position;
		}
		
		// P2 setup
        P2choice = new playerChoice();
        P2choice.finalChoice = null;
        P2choice.icon = GameObject.FindGameObjectWithTag("P2");
		if (P2choice.index >= 0) {
			P2choice.icon.transform.position = P2Pos [P2choice.index].position;
		}

        // P3 setup
        P3choice = new playerChoice();
        P3choice.finalChoice = null;
        P3choice.icon = GameObject.FindGameObjectWithTag("P3");
		if (P3choice.index >= 0) {
			P3choice.icon.transform.position = P3Pos [P3choice.index].position;
		}

        // P4 setup
        P4choice = new playerChoice();
        P4choice.finalChoice = null;
        P4choice.icon = GameObject.FindGameObjectWithTag("P4");
		if (P4choice.index >= 0) {
			P4choice.icon.transform.position = P4Pos [P4choice.index].position;
		}
    }


    // Update is called once per frame
    void Update()
    {
        moveCharacter();
        inputs();
    }

    void inputs()
    {
        Debug.Log("number players selected: " + num_Players_selected);
        // Select Characters
        if (Input.GetButtonDown(P1 + KEY_O) && (p1CanMove == true))
        {
            p1CanMove = false;
            P1choice.finalChoice = selectCharacter(P1choice.index);
            Debug.Log("P1 selected" + P1choice.finalChoice.name);
            PlayerPrefs.SetInt("p1Choice", P1choice.index);
            num_Players_selected++;
        }
        if (Input.GetButtonDown(P2 + KEY_O) && (p2CanMove == true))
        {
            p2CanMove = false;
            P2choice.finalChoice = selectCharacter(P2choice.index);
            Debug.Log("P2 selected" + P2choice.finalChoice.name);
            PlayerPrefs.SetInt("p2Choice", P2choice.index);
            num_Players_selected++;
        }
        if (Input.GetButtonDown(P3 + KEY_O) && (p3CanMove == true))
        {
            p3CanMove = false;
            P3choice.finalChoice = selectCharacter(P3choice.index);
            Debug.Log("P3 selected" + P3choice.finalChoice.name);
            PlayerPrefs.SetInt("p3Choice", P3choice.index);
            num_Players_selected++;
        }
        if (Input.GetButtonDown(P4 + KEY_O) && (p4CanMove == true))
        {
            p4CanMove = false;
            P4choice.finalChoice = selectCharacter(P4choice.index);
            Debug.Log("P4 selected" + P4choice.finalChoice.name);
            PlayerPrefs.SetInt("p4Choice", P4choice.index);
            num_Players_selected++;
        }

        // canceling selection
        if (Input.GetButtonDown(P1 + KEY_A) && p1CanMove == false)
        {
            p1CanMove = true;
            P1choice.finalChoice = null;
			PlayerPrefs.SetInt("p1Choice", -1);
            num_Players_selected--;
        }
        if (Input.GetButtonDown(P2 + KEY_A) && p2CanMove == false)
        {
            p2CanMove = true;
            P2choice.finalChoice = null;
			PlayerPrefs.SetInt("p2Choice", -1);
            num_Players_selected--;
        }
        if (Input.GetButtonDown(P3 + KEY_A) && p3CanMove == false)
        {
            p3CanMove = true;
            P3choice.finalChoice = null;
			PlayerPrefs.SetInt("p3Choice", -1);
            num_Players_selected--;
        }
        if (Input.GetButtonDown(P4 + KEY_A) && p4CanMove == false)
        {
            p4CanMove = true;
            P4choice.finalChoice = null;
			PlayerPrefs.SetInt("p4Choice", -1);
		
            num_Players_selected--;
        }

        // START GAME
        if (Input.GetButtonUp("Start") )
        {
            if (P1choice.finalChoice != null)
            {
                Debug.Log("sending P1 index: " + P1choice.index);
            }
            if (P2choice.finalChoice != null)
            {
                Debug.Log("sending P2 index: " + P2choice.index);
            }
            if (P3choice.finalChoice != null)
            {
                Debug.Log("sending P3 index: " + P3choice.index);
            }
            if (P4choice.finalChoice != null)
            {
                Debug.Log("sending P4 index: " + P4choice.index);
            }
            PlayerPrefs.SetInt("numPlayers", num_Players_selected);
            Application.LoadLevel("Scene1");
        }
    }

    // Select character method
    public GameObject selectCharacter(int i)
    {
        switch (i)
        {
            case Character.WAREWOLF:
                {
                    return Warewolf;
                }
            case Character.KNIGHT:
                {
                    return Knight;
                }
            case Character.RED_BARON:
                {
                    return Red_Baron;
                }
            case Character.ROBOT:
                {
                    return Robot;
                }
            default:
                {
                    return selectCharacter(Random.Range(0, NUM_CHARS - 1));
                }
        }
    }


    void moveCharacter()
    {
        if (Input.GetButtonDown(P1 + "LeftRight") && (p1CanMove == true))
        {
            reposition(P1, P1choice, P1Pos);
        }
        if (Input.GetButtonDown(P2 + "LeftRight") && (p2CanMove == true))
        {
            reposition(P2, P2choice, P2Pos);
        }
        //if (Input.GetButton(P3 + "LeftRight") && p3CanMove)
        //{
        //    reposition(P3, P3choice, P3Pos);
        //}
        //if (Input.GetButton(P4 + "LeftRight") && p4CanMove)
        //{
        //    reposition(P4, P4choice, P4Pos);
        //}
    }

    void reposition(string Player, playerChoice pChoice, Transform[] pPos)
    {
		if (Input.GetAxis(Player + "LeftRight") > 0)
		{
			pChoice.index++;
			if (pChoice.index > NUM_CHARS - 1)
			{
				pChoice.index = 0;
			}
			pChoice.icon.transform.position = pPos[pChoice.index].position;
        }
        else if (Input.GetAxis(Player + "LeftRight") < 0)
        {
			pChoice.index--;
			if (pChoice.index < 0)
			{
				pChoice.index = NUM_CHARS - 1;
			}
			pChoice.icon.transform.position = pPos[pChoice.index].position;
		}
	}
}
