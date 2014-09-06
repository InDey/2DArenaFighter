using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Public variables
    public List<GameObject> Players;
	public GameObject player;

    // Private variables

	// Use this for initialization
	void Start () 
	{
		int character1 = Character.KNIGHT;//TODO get from prefs or something
		int character2 = Character.RED_BARON;//TODO get from prefs or something

		GameObject player1 = (GameObject) Instantiate(player, transform.position, transform.rotation);
		Character char1 = addCharacter (player1, character1);
		char1.setPlayerNumber (0);
		Players.Add (player1);

		GameObject player2 = (GameObject) Instantiate(player, transform.position, transform.rotation);
		Character char2 = addCharacter (player2, character2);
		char2.setPlayerNumber (1);
		Players.Add (player2);
	}

	private Character addCharacter(GameObject player, int character) 
	{
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
