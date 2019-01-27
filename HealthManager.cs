using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public Slider slider;
	public int baseHealth = 10;
	public int maxHealth = 100;
	private int currentHealth;
	private int consecutiveSuccess;
	public int regenThreshold = 1;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = baseHealth;
		consecutiveSuccess = 0;
		slider.maxValue = baseHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		slider.value = currentHealth;
	}

	public void inputSuccess()
	{
		if (consecutiveSuccess >= regenThreshold && currentHealth < maxHealth)
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
		if (currentHealth > 0)
			currentHealth--;
	}

	public bool isDead()
	{
		return currentHealth <= 0;
	}

	public void resetHealth()
	{
		currentHealth = baseHealth;
	}
}
