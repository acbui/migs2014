using UnityEngine;
using System.Collections;

public class Elves : MonoBehaviour {

	public enum elfState
	{
		IDLE = 0,
		GIVING = 1,
		STEALING = 2,
	};
	public elfState currentState;
	public float stateDelay;

	public int cartFood;
	public int stolenFood;
	public int totalFood;
	public bool isStealing;

	public Animator[] elfAnims;

	public void resetFood()
	{
		cartFood = 0;
		stolenFood = 0;
		totalFood = 0;
	}

	public void initialize()
	{
		elfAnims = new Animator[2];
		elfAnims[0] = gameObject.GetComponent<Animator>();
		elfAnims[1] = GameObject.Find ("Tiny Player").GetComponent<Animator>();
		resetFood ();
	}

	public void checkElfStates()
	{
		if (currentState == elfState.IDLE)
		{
			bool LMB = Input.GetMouseButtonDown(0);
			bool RMB = Input.GetMouseButtonDown(1);

			if (LMB || RMB)
			{
				if (LMB)
				{
					currentState = elfState.GIVING;
				}
				else if (RMB)
				{
					isStealing = true;
					currentState = elfState.STEALING;
				}
				switchState (currentState);
			}
		}
	}

	public void addFood(bool stealing)
	{
		if (stealing)
			stolenFood += 1;
		else 
			cartFood += 1;
		totalFood += 1;
	}

	public void switchState(elfState i)
	{
		switch ((int) i)
		{
		case 1:
			foreach (Animator a in elfAnims)
				a.SetBool("isGiving", true);
			break;
		case 2:
			foreach (Animator a in elfAnims)
				a.SetBool("isStealing", true);
			break;
		case 3:
			foreach (Animator a in elfAnims)
				a.SetBool("isDead", true);
			break;
		default:
			break;
		}
		StartCoroutine(resetElves());
	}

	public IEnumerator resetElves()
	{
		yield return new WaitForSeconds(0.75f);
		addFood(isStealing);
		foreach (Animator a in elfAnims)
		{
			a.SetBool("isGiving", false);
			a.SetBool("isStealing", false);
		}
		currentState = (int) elfState.IDLE;
		isStealing = false;
	}
}
