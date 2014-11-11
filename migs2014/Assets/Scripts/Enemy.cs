using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool lookingAway;
	public float currentHunger;
	public float maxHunger;

	public Animator anim;
	public Player player;

	public float minDelay;
	public float maxDelay;

	// Use this for initialization
	void Start () {
		initializeGiant ();
	}
	
	// Update is called once per frame
	void Update () {
		catchSteal ();
		if (!lookingAway)
		{
			StartCoroutine (lookAway (Random.Range (minDelay, maxDelay)));
		}
	}

	void catchSteal()
	{
		if (!lookingAway)
		{
			if (player.stealingItem)
			{
				if (player.lives == 2)
				{
					player.lives = 1;
					player.foodStock = 0;
					anim.SetInteger ("Angry", 1);
					StartCoroutine (backToIdle (0.2f));
				}
				else 
				{
					player.lives = 0;
					anim.SetInteger ("Angry", 2);
					StartCoroutine (backToIdle (0.2f));
				}
			}
		}
	}

	void hungerLevel()
	{
		if (player.foodCart + player.foodStock >= player.maxFood)
		{
			currentHunger = maxHunger; 
		}

		if (currentHunger >= maxHunger)
		{
			if (player.foodCart <= 0)
			{
				if (player.lives == 2)
				{
					player.lives = 1;
					player.foodStock = 0;
					anim.SetInteger ("Angry", 1);
					StartCoroutine (backToIdle (0.2f));
				}
				else 
				{
					player.lives = 0;
					anim.SetInteger ("Angry", 2);
					StartCoroutine (backToIdle (0.2f));
				}
			}
			else {
				player.sendingCart = true;
				anim.SetInteger ("Eat", 1);
			}
		}
		else 
		{
			currentHunger++;
		}
	}

	IEnumerator lookAway(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		lookingAway = true;
		anim.SetInteger ("Look", 1);
		StartCoroutine (lookBack (0.2f));
	}

	IEnumerator backToIdle(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		anim.SetInteger ("Angry", 0);
		if (player.lives == 0)
		{
			GameManager.ins.endGame();
		}
	}

	IEnumerator lookBack(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		anim.SetInteger ("Look", 0);
		lookingAway = false;
	}

	IEnumerator stopEating(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		anim.SetInteger ("Eat", 0);
		currentHunger -= ((float)player.foodCart / (float)player.maxFood)*maxHunger;
		player.foodCart = 0;
		player.sendingCart = false;
	}

	public void initializeGiant()
	{
		lookingAway = false;
		
		player = GameObject.Find ("Player").GetComponent<Player>() as Player;
		anim = gameObject.GetComponent<Animator> ();
		currentHunger = 0;
	}
}
