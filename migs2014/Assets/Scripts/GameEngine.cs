using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	public static GameEngine ins;

	public int score; 
	public Elves player;
	public Giant giant;

	public int numElves;

	public Transform angerBar;
	
	void Awake()
	{
		ins = this;
		DontDestroyOnLoad (this);
		score = 0;
		player = GameObject.FindObjectOfType(typeof(Elves)) as Elves;
		giant = GameObject.FindObjectOfType(typeof(Giant)) as Giant;
		angerBar = GameObject.Find("MeterScale").transform;
	}

	// Use this for initialization
	void Start () {
		numElves = 2;
		giant.initialize();
		player.initialize();
	}
	
	// Update is called once per frame
	void Update () {
		if (!giant.checkHunger())
		{
			giant.currentAnger += (0.01f * giant.MAX_ANGER);
			player.checkElfStates();
		}
		updateAngerMeter();
	}

	public void updateGiantState()
	{
		if (giant.currentState == Giant.giantState.EATING_IDLE)
		{
			StartCoroutine (delayGiantAnimation(0, Giant.giantState.HUNGRY));
			updateGiantState();
		}
		else if (giant.currentState == Giant.giantState.HUNGRY)
		{
			if ((float) player.cartFood/player.totalFood <= 0.5f || player.isStealing)
			{
				print (player.isStealing);
				StartCoroutine (delayGiantAnimation(2.5f, Giant.giantState.NO_FOOD));
			}
			else 
			{
				giant.lessenAnger ((float) player.cartFood/player.totalFood);
				StartCoroutine (delayGiantAnimation(2.5f, Giant.giantState.TAKING_FOOD));
			}
		}
	}

	public void resetElves()
	{
		StartCoroutine (player.resetElves());
	}

	IEnumerator delayGiantAnimation(float delay, Giant.giantState state, bool stealingElves)
	{
		yield return new WaitForSeconds(delay);
		if (stealingElves)
			player.stolenFood = 0;
		giant.currentState = state;
		giant.switchState(giant.currentState);
	}

	public IEnumerator delayEatElf(float delay)
	{
		player.el
		yield return new WaitForSeconds(delay);
		player.stolenFood = 0;
	}

	public void updateAngerMeter()
	{
		angerBar.localScale = new Vector3(giant.currentAnger/giant.MAX_ANGER, angerBar.localScale.y, angerBar.localScale.z);
	}

}
