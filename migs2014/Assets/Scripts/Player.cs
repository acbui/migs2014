using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives; 
	public bool holdingItem; 

	// Use this for initialization
	void Start () {
		lives = 2;
		holdingItem = false; 
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}

}
