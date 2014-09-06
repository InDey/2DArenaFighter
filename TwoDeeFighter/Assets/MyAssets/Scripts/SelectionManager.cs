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

    public bool p1CanMove = true;
    public bool p2CanMove = true;
    public bool p3CanMove = true;
    public bool p4CanMove = true;

    public class playerChoice
    {
        public GameObject icon;
        public int index = 0;
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
        //TODO make these start at the starting position until you move

        // P1 setup
        P1choice = new playerChoice();
        P1choice.icon = GameObject.FindGameObjectWithTag("P1");
        P1choice.icon.transform.position = P1Pos[P1choice.index].position;

        // P2 setup
        P2choice = new playerChoice();
        P2choice.icon = GameObject.FindGameObjectWithTag("P2");
        P2choice.icon.transform.position = P2Pos[P2choice.index].position;

        // P3 setup
        P3choice = new playerChoice();
        P3choice.icon = GameObject.FindGameObjectWithTag("P3");
        P3choice.icon.transform.position = P3Pos[P3choice.index].position;

        // P4 setup
        P4choice = new playerChoice();
        P4choice.icon = GameObject.FindGameObjectWithTag("P4");
        P4choice.icon.transform.position = P4Pos[P4choice.index].position;

        InvokeRepeating("moveCharacter", .2f, .2f);
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("number players selected: " + num_Players_selected);
        // Select Characters
        if (Input.GetButtonDown(P1 + KEY_O) && p1CanMove)
        {
            p1CanMove = false;
            P1choice.finalChoice = selectCharacter(P1choice.index);
            Debug.Log("P1 selected" + P1choice.finalChoice.name);
            PlayerPrefs.SetInt("p1Choice", P1choice.index);
            num_Players_selected++;
        }
        if (Input.GetButtonDown(P2 + KEY_O) && p2CanMove)
        {
            p2CanMove = false;
            P2choice.finalChoice = selectCharacter(P2choice.index);
            Debug.Log("P2 selected" + P2choice.finalChoice.name);
            PlayerPrefs.SetInt("p2Choice", P2choice.index);
            num_Players_selected++;
        }
        if (Input.GetButtonDown(P3 + KEY_O) && p3CanMove)
        {
            p3CanMove = false;
            P3choice.finalChoice = selectCharacter(P3choice.index);
            Debug.Log("P3 selected" + P3choice.finalChoice.name);
            PlayerPrefs.SetInt("p3Choice", P3choice.index);
            num_Players_selected++;
        }
        if (Input.GetButtonDown(P4 + KEY_O) && p4CanMove)
        {
            p4CanMove = false;
            P4choice.finalChoice = selectCharacter(P4choice.index);
            Debug.Log("P4 selected" + P4choice.finalChoice.name);
            PlayerPrefs.SetInt("p4Choice", P4choice.index);
            num_Players_selected++;
        }

        // canceling selection
        if (Input.GetButtonDown(P1 + KEY_A) && !p1CanMove)
        {
            p1CanMove = true;
            P1choice.finalChoice = null;
            num_Players_selected--;
        }
        if (Input.GetButtonDown(P2 + KEY_A) && !p2CanMove)
        {
            p2CanMove = true;
            P2choice.finalChoice = null;
            num_Players_selected--;
        }
        if (Input.GetButtonDown(P3 + KEY_A) && !p3CanMove)
        {
            p3CanMove = true;
            P3choice.finalChoice = null;
            num_Players_selected--;
        }
        if (Input.GetButtonDown(P4 + KEY_A) && !p4CanMove)
        {
            p4CanMove = true;
            P4choice.finalChoice = null;
            num_Players_selected--;
        }

        // START GAME
        if (Input.GetButtonUp("Start") && (num_Players_selected > 1))
        {
            
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
        if (Input.GetButton(P1 + HORIZ) && p1CanMove)
        {
            reposition(P1, P1choice, P1Pos);
        }
        if (Input.GetButton(P2 + HORIZ) && p2CanMove)
        {
            reposition(P2, P2choice, P2Pos);
        }
        if (Input.GetButton(P3 + HORIZ) && p3CanMove)
        {
            reposition(P3, P3choice, P3Pos);
        }
        if (Input.GetButton(P4 + HORIZ) && p4CanMove)
        {
            reposition(P4, P4choice, P4Pos);
        }
    }

    void reposition(string Player, playerChoice pChoice, Transform[] pPos)
    {
        if (Input.GetAxis(Player + HORIZ) > 0)
        {
            if (pChoice.index == NUM_CHARS - 1)
            {
                pChoice.index = 0;
                pChoice.icon.transform.position = pPos[pChoice.index].position;
            }
            else
            {
                pChoice.icon.transform.position = pPos[++pChoice.index].position;
            }
        }
        else if (Input.GetAxis(P1 + HORIZ) < 0)
        {
            if (pChoice.index == 0)
            {
                pChoice.index = NUM_CHARS - 1;
                pChoice.icon.transform.position = pPos[pChoice.index].position;
            }
            else
            {
                pChoice.icon.transform.position = pPos[--pChoice.index].position;
            }
        }
    }
}
