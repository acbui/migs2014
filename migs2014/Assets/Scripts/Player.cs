using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives; 
	public int foodStock;
	public int foodCart; 
	public int maxFood;

	public bool stealingItem; 
	public bool sendingItem;
	public bool sendingCart;
	
	public float delay; 
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
		if (Input.GetMouseButtonDown(0))
		{
			if (!stealingItem)
			{
				stealingItem = true;
				anim.SetInteger ("Steal", 1);
				StartCoroutine (safeSteal());
			}
		}
	}

	void sendItem()
	{
		if (Input.GetMouseButtonDown (1))
		{
			if (!sendingItem)
			{
				foodCart++;
				sendingItem = true;
				anim.SetInteger ("Send", 1);
				StartCoroutine (endSend(0.5f));
			}
		}
	}

	IEnumerator endSend (float pDelay)
	{
		yield return new WaitForSeconds (pDelay);
		anim.SetInteger ("Send", 0);
		sendingItem = false; 
	}

	IEnumerator safeSteal()
	{
		yield return new WaitForSeconds (delay);
		anim.SetInteger ("Steal", 0);
		foodStock++;
		GameManager.ins.score = foodStock;
		stealingItem = false; 
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
	}
}
