using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager ins;

	public int score; 
	public Player player;
	public Enemy giant;

	public Transform hungerbar;

	void Awake()
	{
		ins = this;
		DontDestroyOnLoad (this);
		score = 0;
		player = GameObject.Find ("Player").GetComponent<Player> ();
		giant = GameObject.Find ("Giant").GetComponent<Enemy> ();
	}


	// Use this for initialization
	void Start () {
		player.initializePlayer ();
		giant.initializeGiant ();
	}
	
	// Update is called once per frame
	void Update () {
		updateHunger ();
	}

	void updateHunger()
	{
		hungerbar.localScale = new Vector3 (giant.currentHunger / giant.maxHunger, hungerbar.localScale.y, hungerbar.localScale.z);
	}

	public void endGame()
	{
		print ("end");
		Application.LoadLevel ("Scores");
	}
}
