using UnityEngine;
using System.Collections;

public class SwitchScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space))
		{
			if (Application.loadedLevelName.Equals ("Title"))
				Application.LoadLevel ("Instructions");
			if (Application.loadedLevelName.Equals ("Instructions"))
			{
				if (GameObject.Find("FirstInstructions") != null)
				{
					Destroy (GameObject.Find("FirstInstructions"));
				}
				else 
					Application.LoadLevel ("Main");
			}
			if (Application.loadedLevelName.Equals ("Scores"))
				Application.LoadLevel ("Title");

		}
	}
}
