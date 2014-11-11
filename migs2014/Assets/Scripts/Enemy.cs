using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool lookingAtElves;
	public float currentHunger;
	public float maxHunger;

	public bool hungry;

	public Animator anim;
	public Animator cartAnim;
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
			if (!lookingAtElves)
			{
				//StartCoroutine (lookAway ((float) Random.Range (minDelay, maxDelay)));
			}

			if (lookingAtElves || hungry)
			{
				print ("turn around");
				anim.SetInteger ("Look", 1);
				if (player.foodCart <= 0 || player.stealingItem)
				{
					getMad ();
				}
				else 
				{
					print ("cart");
					player.sendingCart = true;
					cartAnim.SetInteger ("FeedGiant", 1);
					lookingAtElves = false;
					StartCoroutine (stopEating (1.0f));
				}
			}
		}
	}

	// update hunger level
	void hungerLevel()
	{
		if (player.foodCart + player.foodStock >= player.maxFood)
		{
			currentHunger = maxHunger; 
		}

		if (currentHunger >= maxHunger)
		{
			hungry = true;
		}
		else 
		{
			currentHunger += 0.5f;
		}
	}

	void getMad()
	{
		print ("mad");
		anim.SetInteger ("Angry", 1);
		currentHunger = 0;
		if (player.lives == 2)
		{
			player.lives = 1;
			player.foodStock = 0;
			GameManager.ins.score = player.foodStock;
			StartCoroutine (stopLooking (0.5f));
			lookingAtElves = false;
		}
		else if (player.lives == 1)
		{
			player.lives = 0;
			GameManager.ins.endGame();
		}
		StartCoroutine (stopAnger (0.4f));
		hungry = false;
	}

	// if giant eats cart food
	IEnumerator stopEating(float pDelay)
	{
		print ("stop eating");
		currentHunger -= ((float)player.foodCart / (float)player.maxFood)*maxHunger;
		player.foodCart = 0;
		player.sendingCart = false;
		yield return new WaitForSeconds(pDelay);
		print ("stopped eating after " + pDelay);
		anim.SetInteger ("Look", 0);
		cartAnim.SetInteger ("FeedGiant", 0);
		player.updateCart ();
		hungry = false;
	}

	IEnumerator stopLooking (float pDelay)
	{
		yield return new WaitForSeconds(pDelay);
		print ("stopped looking after " + pDelay);
		anim.SetInteger ("Look", 0);
	}

	IEnumerator lookAway(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		lookingAtElves = true;
	}

	IEnumerator stopAnger (float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		anim.SetInteger ("Angry", 0);
	}

	IEnumerator stopCart(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		cartAnim.SetInteger ("FeedGiant", 1);
	}

	public void initializeGiant()
	{
		lookingAtElves = false;
		
		player = GameObject.Find ("Player").GetComponent<Player>() as Player;
		anim = gameObject.GetComponent<Animator> ();
		currentHunger = 0;
		cartAnim = GameObject.Find ("wheelbarrow").GetComponent<Animator> ();
		cartAnim.SetInteger ("FeedGiant", 0);
	}
}
