using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Public variables
    public List<GameObject> Players;
	public GameObject player;
    public Sprite spriteIdle;

    // Private variables

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.HasKey("p1Choice"))
		{
			int p1Choice = PlayerPrefs.GetInt("p1Choice");
            Debug.Log("P1 has a choice of:" + p1Choice);
			if (p1Choice >= 0) {
				GameObject player1 = (GameObject) Instantiate(player, transform.position, transform.rotation);
				Character character = addCharacter (player1, p1Choice);
				character.setPlayerNumber (0);
				Players.Add (player1);
			}
		}

		if (PlayerPrefs.HasKey("p2Choice"))
		{
			int p2Choice = PlayerPrefs.GetInt("p2Choice");
            Debug.Log("P2 has a choice of:" + p2Choice);
			if (p2Choice >= 0) {
				GameObject player2 = (GameObject) Instantiate(player, transform.position, transform.rotation);
				Character character = addCharacter (player2, p2Choice);
				character.setPlayerNumber (1);
				Players.Add (player2);
			}
		}

		if (PlayerPrefs.HasKey("p3Choice"))
		{
			int p3Choice = PlayerPrefs.GetInt("p3Choice");
			if (p3Choice >= 0) {
				GameObject player3 = (GameObject) Instantiate(player, transform.position, transform.rotation);
				Character character = addCharacter (player3, p3Choice);
				character.setPlayerNumber (2);
				Players.Add (player3);
			}
		}

		if (PlayerPrefs.HasKey("p4Choice"))
		{
			int p4Choice = PlayerPrefs.GetInt("p4Choice");
			if (p4Choice >= 0) {
				GameObject player4 = (GameObject) Instantiate(player, transform.position, transform.rotation);
				Character character = addCharacter (player4, p4Choice);
				character.setPlayerNumber (3);
				Players.Add (player4);
			}
		}
	}

	private Character addCharacter(GameObject player, int character) 
	{
		Debug.Log ("Char: " + character);
		switch (character)
		{
		case Character.WAREWOLF:
		{
			return player.AddComponent<CharacterWarewolf>();
		} 
		case Character.KNIGHT:
		{
			return player.AddComponent<CharacterKnight>();
		} 
		case Character.RED_BARON:
        {
            SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
            renderer.sprite = spriteIdle;
			return player.AddComponent<CharacterRedBaron>();
		} 
		case Character.ROBOT:
		{
			return player.AddComponent<CharacterRobot>();
		} 
		}
		return null;
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
