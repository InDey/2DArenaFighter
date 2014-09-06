using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

    public static readonly string P1 = "P1";
    public static readonly string P2 = "P2";
    public static readonly string P3 = "P3";
    public static readonly string P4 = "P4";

    public static readonly string VERT = "Vertical";
    public static readonly string HORIZ = "Horizontal";
    public static readonly string KEY_O = "KeyO";
    public static readonly string KEY_U = "KeyU";
    public static readonly string KEY_Y = "KeyY";
    public static readonly string KEY_A = "KeyA";
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown(P1+KEY_O))
        {
            Application.LoadLevel("characterSelect");
        }
	}
}
