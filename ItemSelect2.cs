using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect2 : MonoBehaviour
{

	public Button replayButton;
	public Button menuButton;

	private Color replayColor;
	private Color menuColor;
	
	// Use this for initialization
	void Start ()
	{
		replayColor = replayButton.GetComponentInChildren<Text>().color;
		menuColor = menuButton.GetComponentInChildren<Text>().color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onSelectReplay()
	{
		Color newColor = new Color(replayColor.r, replayColor.g, replayColor.b, 1);
		replayButton.GetComponentInChildren<Text>().color = newColor;
		replayButton.GetComponentInChildren<Text>().fontSize += 10;

	}
	
	public void onDeselectReplay()
	{
		replayButton.GetComponentInChildren<Text>().color = replayColor;
		replayButton.GetComponentInChildren<Text>().fontSize -= 10;

	}
		
	public void onSelectMenu()
	{
		Color newColor = new Color(menuColor.r, menuColor.g, menuColor.b, 1);
		menuButton.GetComponentInChildren<Text>().color = newColor;
		menuButton.GetComponentInChildren<Text>().fontSize += 10;

	}
	
	public void onDeselectMenu()
	{
		menuButton.GetComponentInChildren<Text>().color = menuColor;
		menuButton.GetComponentInChildren<Text>().fontSize -= 10;

	}
	
}
