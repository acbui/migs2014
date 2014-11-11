using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives; 
	public int foodStock;
	public int foodCart; 
	public int maxFood;

	public SpriteRenderer[] foods;
	public Sprite[] bags; 
	public SpriteRenderer friend;
	public SpriteRenderer bag;

	public SpriteRenderer[] foodPiles;

	public bool stealingItem; 
	public bool sendingItem;
	public bool sendingCart;

	public Animator anim;
	public Animator cartAnim;


	// Use this for initialization
	void Start () {
		initializePlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!sendingCart)
		{ 
			sendItem ();
		}
		stealItem ();
	}

	void stealItem()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (!stealingItem)
			{
				stealingItem = true;
				anim.SetInteger ("Steal", 1);
				StartCoroutine (safeSteal(0.7f));
			}
		}
	}

	void sendItem()
	{
		if (Input.GetMouseButtonDown (0))
		{
			if (!sendingItem)
			{
				sendingItem = true;
				anim.SetInteger ("Send", 1);
				StartCoroutine (endSend(0.7f));
			}
		}
	}

	IEnumerator endSend (float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		foodCart++;
		updateCart ();
		updatePile ();
		anim.SetInteger ("Send", 0);
		sendingItem = false; 
	}

	IEnumerator safeSteal(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		anim.SetInteger ("Steal", 0);
		foodStock++;
		updateBag ();
		updatePile ();
		GameManager.ins.score = foodStock;
		stealingItem = false; 
	}

	public void updateBag()
	{

		if (foodStock <= 4)
		{
			bag.sprite = bags[foodStock];
		}
	}

	public void updateCart()
	{
		if (foodCart == 0)
		{
			foreach (SpriteRenderer s in foods)
			{
				s.enabled = false;
			}
		}
		else if (foodCart == 1)
		{
			foods[0].enabled = true;
		}
		else if (foodCart >= 2 && foodCart < 4)
		{
			for (int i = 0; i < foodCart; i++)
			{
				if (i == foodCart-1)
				{
					foods[i].enabled = true;
				}
				else 
				{
					foods[i].enabled = false;
				}
			}
		}
	}

	public void updatePile()
	{
		if (foodCart + foodStock == 0)
		{
			foodPiles[0].enabled = true;
		}
		else if (foodCart + foodStock >= 1 && foodCart + foodStock < 5)
		{
			for (int i = 0; i < foodCart + foodStock; i++)
			{
				if (i == foodCart + foodStock-1)
				{
					foodPiles[i].enabled = true;
				}
				else 
				{
					foodPiles[i].enabled = false;
				}
			}
		}
		else
		{
			foreach (SpriteRenderer s in foodPiles)
			{
				foodPiles[0].enabled = false;
			}
		}
	}

	public void byeFriend()
	{
		friend.enabled = false;
	}

	public void initializePlayer()
	{
		lives = 2;
		stealingItem = false; 
		sendingItem = false;
		sendingCart = false;
		foodCart = 0;
		foodStock = 0;
		anim = gameObject.GetComponent <Animator> ();
		anim.SetInteger ("Send", 0);
		anim.SetInteger ("Steal", 0);
		updatePile ();
	}
}
