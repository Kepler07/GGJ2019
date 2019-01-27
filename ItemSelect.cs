using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{

	public Button startButton;
	public Button quitButton;

	private Color startColor;
	private Color quitColor;
	
	// Use this for initialization
	void Start ()
	{
		startColor = startButton.GetComponentInChildren<Text>().color;
		quitColor = quitButton.GetComponentInChildren<Text>().color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onSelectStart()
	{
		Color newColor = new Color(startColor.r, startColor.g, startColor.b, 1);
		startButton.GetComponentInChildren<Text>().color = newColor;
		startButton.GetComponentInChildren<Text>().fontSize += 10;

	}
	
	public void onDeselectStart()
	{
		startButton.GetComponentInChildren<Text>().color = startColor;
		startButton.GetComponentInChildren<Text>().fontSize -= 10;

	}
		
	public void onSelectQuit()
	{
		Color newColor = new Color(quitColor.r, quitColor.g, quitColor.b, 1);
		quitButton.GetComponentInChildren<Text>().color = newColor;
		quitButton.GetComponentInChildren<Text>().fontSize += 10;

	}
	
	public void onDeselectQuit()
	{
		quitButton.GetComponentInChildren<Text>().color = quitColor;
		quitButton.GetComponentInChildren<Text>().fontSize -= 10;

	}
	
}
