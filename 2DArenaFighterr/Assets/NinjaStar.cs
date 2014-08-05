using UnityEngine;
using System.Collections;

public class NinjaStar : MonoBehaviour {

	public GameObject ninjaStar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "Platform") {
			Destroy (ninjaStar);
		}
	}
}
