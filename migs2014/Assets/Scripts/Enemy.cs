using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool lookingAway;
	public float currentHunger;
	public float maxHunger;

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
		if (!lookingAway)
		{
			catchSteal ();
			StartCoroutine (lookAway ((float) Random.Range (minDelay, maxDelay)));
		}
	}

	void catchSteal()
	{
		if (player.stealingItem)
		{
			getMad ();
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
				getMad();
			}
			else {
				player.sendingCart = true;
				anim.SetInteger ("Eat", 1);
				StartCoroutine (stopEating(0.2f));
			}
		}
		else 
		{
			currentHunger++;
		}
	}

	void getMad()
	{
		if (player.lives == 2)
		{
			player.lives = 1;
			player.foodStock -= player.foodStock/2;
			anim.SetInteger ("Angry", 1);
			StartCoroutine (backToIdle (0.2f));
		}
		else if (player.lives == 1)
		{
			player.lives = 0;
			anim.SetInteger ("Angry", 2);
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
		if (player.lives == 0)
		{
			GameManager.ins.endGame();
		}
	}

	IEnumerator stopEating(float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		anim.SetInteger ("Eat", 0);
		currentHunger -= ((float)player.foodCart / (float)player.maxFood)*maxHunger;
		player.foodCart = 0;
		player.sendingCart = false;
		player.updateCart ();
	}

	IEnumerator lookAway(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		lookingAway = true;
		anim.SetInteger ("Look", 1);
		StartCoroutine (lookBack (0.2f));
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
