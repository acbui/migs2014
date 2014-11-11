using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool lookingAtElves;
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
			currentHunger++;
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
			anim.SetInteger ("Look", 0);
			lookingAtElves = false;
		}
		else if (player.lives == 1)
		{
			player.lives = 0;
			GameManager.ins.endGame();
		}
		anim.SetInteger ("Angry", 0);
		hungry = false;
	}

	// if giant eats cart food
	IEnumerator stopEating(float pDelay)
	{
		print ("stop eating");
		currentHunger -= ((float)player.foodCart / (float)player.maxFood)*maxHunger;
		player.foodCart = 0;
		player.sendingCart = false;
		player.updateCart ();
		anim.SetInteger ("Look", 0);
		yield return new WaitForSeconds(pDelay);
		print ("stopped eating after " + pDelay);
		hungry = false;
	}

	IEnumerator lookAway(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		lookingAtElves = true;
	}

	public void initializeGiant()
	{
		lookingAtElves = false;
		
		player = GameObject.Find ("Player").GetComponent<Player>() as Player;
		anim = gameObject.GetComponent<Animator> ();
		currentHunger = 0;
	}
}
