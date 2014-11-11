using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager ins;

	public int score; 

	void Awake()
	{
		ins = this;
		DontDestroyOnLoad (this);
		score = 0;
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endGame()
	{
		Application.LoadLevel ("Scores");
	}
}
