using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

	public int baseHealth = 10;
	private int currentHealth;
	private int consecutiveSuccess;
	public int regenThreshold = 4;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = baseHealth;
		consecutiveSuccess = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void inputSuccess()
	{
		if (consecutiveSuccess >= regenThreshold)
		{
			currentHealth++;
			consecutiveSuccess = 0;
		}
		else
		{
			consecutiveSuccess++;
		}
	}
	
	public void inputFail()
	{
		consecutiveSuccess = 0;
		currentHealth--;
	}

	public bool isDead()
	{
		return currentHealth == 0;
	}

	public void resetHealth()
	{
		currentHealth = baseHealth;
	}
}
