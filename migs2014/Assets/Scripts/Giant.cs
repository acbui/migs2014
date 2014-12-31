using UnityEngine;
using System.Collections;

public class Giant : MonoBehaviour {

	public readonly float MAX_ANGER = 1.0f;
	public float currentAnger;

	public enum giantState 
	{
		HUNGRY = 0,
		TAKING_FOOD = 1,
		NO_FOOD = 2,
		TAKING_ELF = 3,
		EATING_IDLE = 4,
	};
	public giantState currentState;
	
	public float hungerTimer;
	public float minTimer = 2.0f;
	public float maxTimer = 5.0f;
	public float currentTime;

	public Animator giantAnim;

	public void initialize()
	{
		giantAnim = gameObject.GetComponent<Animator>();
		resetGiant();
		currentAnger = 0;
	}

	public void randomHunger()
	{
		hungerTimer = Time.time + Random.Range (minTimer, maxTimer);
	}

	public bool checkHunger()
	{
		if (currentState == giantState.EATING_IDLE)
		{
			currentTime = Time.time;
			
			if (currentTime >= hungerTimer)
			{
				GameEngine.ins.updateGiantState();
				return true;
			}
		}
		return false;
	}

	public void switchState(giantState i)
	{
		switch ((int) i)
		{
		case (int) giantState.HUNGRY:
			giantAnim.SetBool("isHungry", true);
			break;
		case (int) giantState.TAKING_FOOD:
			giantAnim.SetBool("hasEaten", true);
			break;

		case (int) giantState.NO_FOOD:
			giantAnim.SetBool("noFood", true);
			StartCoroutine (GameEngine.ins.delayEatElf(0.79f));
			break;

		case (int) giantState.TAKING_ELF:
			giantAnim.SetBool("hasEatenElf", true);
			break;

		case (int) giantState.EATING_IDLE:
			resetGiant();
			break;

		default:
			break;
		}
		GameEngine.ins.updateGiantState();
	}

	public void resetGiant()
	{
		currentState = giantState.EATING_IDLE;
		randomHunger();
		giantAnim.SetBool("isHungry", false);
		giantAnim.SetBool("hasEaten", false);
		giantAnim.SetBool("noFood", false);
		giantAnim.SetBool("hasEatenElf", false);
	}

	
	public void lessenAnger(float decrease)
	{
		if (currentAnger - decrease <= 0)
		{
			currentAnger = 0;
		}
		else 
			currentAnger -= decrease;
	}

	public void increaseAnger()
	{
		if (currentAnger >= MAX_ANGER)
			currentAnger = MAX_ANGER;
		else 
			currentAnger += (0.01f * MAX_ANGER);
	}
}
