using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives; 
	public int foodStock;
	public int foodCart; 
	public int maxFood;

	public SpriteRenderer[] foods;

	public bool stealingItem; 
	public bool sendingItem;
	public bool sendingCart;

	public Animator anim;

	// Use this for initialization
	void Start () {
		initializePlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!sendingCart)
		{
			stealItem (); 
			sendItem ();
		}
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
				foodCart++;
				updateCart ();
				sendingItem = true;
				anim.SetInteger ("Send", 1);
				StartCoroutine (endSend(0.7f));
			}
		}
	}

	IEnumerator endSend (float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		anim.SetInteger ("Send", 0);
		sendingItem = false; 
	}

	IEnumerator safeSteal(float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		anim.SetInteger ("Steal", 0);
		foodStock++;
		//GameManager.ins.score = foodStock;
		stealingItem = false; 
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
		else if (foodCart >= 2)
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

//		foods = new SpriteRenderer[4]; 
	}
}
