using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool lookingAway;
	public float currentHunger;
	public float maxHunger;

	public bool hungry;

	public Animator anim;
	public Player player;

	public int minDelay;
	public int maxDelay;
	
	void Start () 
	{
		initializeGiant ();
	}

	void Update () 
	{
		if (!hungry)
		{
			hungerLevel ();
			if (!lookingAway)
			{
				StartCoroutine (lookAway ((float) Random.Range (minDelay, maxDelay)));
			}
		}
	}

	void catchSteal()
	{
		if (player.stealingItem)
		{
			getMad ();
		}
		else 
		{
			StartCoroutine (lookBack (2.0f));
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
			hungry = true;
			if (player.foodCart <= 0)
			{
				lookingAway = true;
				anim.SetInteger ("Look", 1);
				getMad ();
				currentHunger = 0;
			}
			else {
				player.sendingCart = true;
				lookingAway = true;
				anim.SetInteger ("Look", 1);
				StartCoroutine (stopEating (1.0f));
				StartCoroutine (lookBack (2.0f));
			}
		}
		else 
		{
			currentHunger++;
		}
	}

	void getMad()
	{
		anim.SetInteger ("Angry", 1);
		if (player.lives == 2)
		{
			player.lives = 1;
			player.foodStock -= player.foodStock/2;
			StartCoroutine (backToIdle (0.2f));
			StartCoroutine (lookBack (1.5f));
		}
		else if (player.lives == 1)
		{
			player.lives = 0;
			StartCoroutine (backToIdle (0.2f));
		}
		else if (player.lives == 0)
		{
			GameManager.ins.endGame ();
		}
	}

	IEnumerator backToIdle(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		anim.SetInteger ("Angry", 0);
		hungry = false;
		if (player.lives == 0)
		{
			GameManager.ins.endGame();
		}
	}

	IEnumerator stopEating(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		currentHunger -= ((float)player.foodCart / (float)player.maxFood)*maxHunger;
		player.foodCart = 0;
		player.sendingCart = false;
		hungry = false;
		player.updateCart ();
	}

	IEnumerator lookAway(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		lookingAway = true;
		anim.SetInteger ("Look", 1);
		catchSteal ();
	}
	
	IEnumerator lookBack(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		anim.SetInteger ("Look", 0);
		lookingAway = false;
	}

	public void initializeGiant()
	{
		lookingAway = false;
		
		player = GameObject.Find ("Player").GetComponent<Player>() as Player;
		anim = gameObject.GetComponent<Animator> ();
		currentHunger = 0;
	}
}
