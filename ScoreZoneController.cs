using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZoneController : MonoBehaviour
{
	public int baseScore = 100;
	private float totalScore;
	public float powFactor = 1.15f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().MonsterList.Count > 0)
		{
			var playerController = other.gameObject.GetComponent<PlayerController>();
			totalScore += calculateScore(playerController.MonsterList);
			Debug.Log("Score: " + calculateScore(playerController.MonsterList));
			Debug.Log("TotalScore: " + totalScore);
			foreach (var monster in playerController.MonsterList)
			{
				Destroy(monster.gameObject);
			}
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
}
