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

    public Transform[] P1Pos = new Transform[NUM_CHARS];
    public Transform[] P2Pos = new Transform[NUM_CHARS];
    public Transform[] P3Pos = new Transform[NUM_CHARS];
    public Transform[] P4Pos = new Transform[NUM_CHARS];

    public class playerChoice
    {
        public GameObject icon;
        public int index = 0;
    }

    public playerChoice P1choice;
    public playerChoice P2choice;
    public playerChoice P3choice;
    public playerChoice P4choice;

    void Awake()
    {
        // P1 setup
        P1choice = new playerChoice();
        P1choice.icon = GameObject.FindGameObjectWithTag("P1");
        P1choice.icon.transform.position = P1Pos[P1choice.index].position;
        InvokeRepeating("moveCharacter(P1, P1choice, P1Pos)", .25f, .25f);
        // P2 setup
        P2choice = new playerChoice();
        P2choice.icon = GameObject.FindGameObjectWithTag("P2");
        P2choice.icon.transform.position = P2Pos[P2choice.index].position;
        //InvokeRepeating("moveCharacter(P2, P2choice, P2Pos)", .5f, .5f);
        // P3 setup
        P3choice = new playerChoice();
        P3choice.icon = GameObject.FindGameObjectWithTag("P3");
        P3choice.icon.transform.position = P3Pos[P3choice.index].position;
        //InvokeRepeating("moveCharacter(P3, P3choice, P3Pos)", .5f, .5f);
        // P4 setup
        P4choice = new playerChoice();
        P4choice.icon = GameObject.FindGameObjectWithTag("P4");
        P4choice.icon.transform.position = P4Pos[P4choice.index].position;
        //InvokeRepeating("moveCharacter(P4, P4choice, P4Pos)", .5f, .5f);
    }


    // Update is called once per frame
    void Update()
    {
        moveCharacter(P1, P1choice, P1Pos);
        moveCharacter(P2, P1choice, P1Pos);
        moveCharacter(P3, P1choice, P1Pos);
        moveCharacter(P4, P1choice, P1Pos);
    }

    void moveCharacter(string Player, playerChoice pChoice, Transform[] pPos)
    {
        if (Input.GetButtonDown(Player + HORIZ))
        {
            if (Input.GetAxis(Player + HORIZ) > 0)
            {
                if (pChoice.index == NUM_CHARS - 1)
                {
                    pChoice.index = 0;
                    pChoice.icon.transform.position = pPos[P1choice.index].position;
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
}
