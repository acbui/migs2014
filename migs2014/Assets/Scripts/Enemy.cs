using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool lookingAtElves;
	public float currentHunger;
	public float maxHunger;

	public bool hungry;

	public Animator anim;
	public Animator cartAnim;
	public Animator bagAnim;
	public SpriteRenderer bagbearer;
	public Player player;

	public int minDelay;
	public int maxDelay;

	public float eatDelay;
	public float angerDelay;
	public float deadDelay;
	public float lookDelay;
	
	void Start () 
	{
		initializeGiant ();
	}

	void Update () 
	{
		if (!hungry)
		{
			hungerLevel ();

			if (hungry)
			{
				print ("turn around");
				anim.SetInteger ("Look", 1);
				StartCoroutine (stopLooking ());
				if (player.foodCart <= 0 || player.stealingItem)
				{
					getMad ();
				}
				else 
				{
					print ("cart");
					player.sendingCart = true;
					cartAnim.SetInteger ("FeedGiant", 1);
					bagbearer.enabled = true;
					bagAnim.SetInteger ("TakeBag", 1);
					lookingAtElves = false;
					StartCoroutine (stopEating ());
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

	public void getMad()
	{
		print ("mad");
		anim.SetInteger ("Angry", 1);
		currentHunger = 0;
		StartCoroutine (stopAnger ());
		hungry = false;
	}

	// if giant eats cart food
	IEnumerator stopEating()
	{
		print ("stop eating");
		currentHunger -= ((float)player.foodCart / (float)player.maxFood)*maxHunger;
		player.foodCart = 0;
		yield return new WaitForSeconds(eatDelay);
		player.sendingCart = false;
		anim.SetInteger ("Look", 0);
		cartAnim.SetInteger ("FeedGiant", 0);
		bagAnim.SetInteger ("TakeBag", 0);
		GameManager.ins.score += player.foodStock;
		player.foodStock = 0;
		player.updateBag ();
		player.updateCart ();
		hungry = false;
	}


	IEnumerator stopLooking ()
	{
		yield return new WaitForSeconds(lookDelay);
		anim.SetInteger ("Look", 0);
	}

	IEnumerator stopAnger ()
	{
		yield return new WaitForSeconds (angerDelay);
		anim.SetInteger ("Angry", 0);
		if (player.lives == 2)
		{
			player.lives = 1;
			player.byeFriend ();
			player.foodStock = 0;
			player.updateBag ();
			GameManager.ins.score += player.foodStock;
			lookingAtElves = false;
		}
		else if (player.lives == 1)
		{
			player.lives = 0;
			StartCoroutine (lastLife());
		}

	}

	IEnumerator lastLife()
	{
		player.anim.SetBool ("Dead", true);
		yield return new WaitForSeconds (deadDelay);
		GameManager.ins.endGame();
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
