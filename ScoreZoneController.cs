using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AppleTV;
using UnityEngine;
using UnityEngine.UI;

// TODO: apply a multiplier at the end of the game based on the player's HP gauge

public class ScoreZoneController : MonoBehaviour
{

	public Text scoreUI;
	public Text scoreEffectText;
	public ParticleSystem scoreParticle;
	
	public int baseScore = 100;
	private float totalScore;
	public float powFactor = 1.15f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreUI.text = "" + (int) totalScore;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().MonsterList.Count > 0)
		{
			var playerController = other.gameObject.GetComponent<PlayerController>();
			var currentScoreToAdd = calculateScore(playerController.MonsterList);
			totalScore += currentScoreToAdd;
			Debug.Log("Score: " + currentScoreToAdd);
			Debug.Log("TotalScore: " + totalScore);
			foreach (var monster in playerController.MonsterList)
			{
				Destroy(monster.gameObject);
			}
			scoreEffectText.text = "+ " + currentScoreToAdd;
			scoreParticle.Play();
			playerController.MonsterList.Clear();
		}
	}

	private float calculateScore(List<GameObject> monsterList)
	{		
		List<MonsterColor> colorList = new List<MonsterColor>();

		foreach (var monster in monsterList)
		{
			MonsterColor monsterColor = monster.GetComponent<MonsterController>().color;
			if (!colorList.Contains(monsterColor))
				colorList.Add(monsterColor);
		}

		int nbColors = colorList.Count;
		int nbMonsters = monsterList.Count;

		return Mathf.Round((baseScore * nbMonstersMultiplier(nbMonsters) * nbColorsMultiplier(nbColors))/ 50.0f) * 50;
	}

	private float nbMonstersMultiplier(int nbMonsters)
	{
		if (nbMonsters == 1)
			return 1;
		return nbMonsters + Mathf.Pow(powFactor, nbMonsters) - 1;
	}
	
	private float nbColorsMultiplier(int nbColors)
	{
		if (nbColors == 1)
			return 1;
		return Mathf.Pow(powFactor, nbColors);
	}

	public void finalizeScore(int finalHealth)
	{
		totalScore += Mathf.Round((totalScore * (float)finalHealth / 100f)/ 50.0f) * 50;
	}
}
