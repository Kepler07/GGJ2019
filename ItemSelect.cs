using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{

	public Button startButton;
	public Button quitButton;
	public Button creditsButton;

	private Color startColor;
	private Color quitColor;
	private Color creditsColor;
	
	// Use this for initialization
	void Start ()
	{
		startColor = startButton.GetComponentInChildren<Text>().color;
		quitColor = quitButton.GetComponentInChildren<Text>().color;
		creditsColor = creditsButton.GetComponentInChildren<Text>().color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onSelectStart()
	{
		startButton.GetComponentInChildren<Text>().color = Color.red;
	}
	
	public void onDeselectStart()
	{
		startButton.GetComponentInChildren<Text>().color = startColor;
	}
		
	public void onSelectQuit()
	{
		quitButton.GetComponentInChildren<Text>().color = Color.red;
	}
	
	public void onDeselectQuit()
	{
		quitButton.GetComponentInChildren<Text>().color = quitColor;
	}
		
	public void onSelectCredits()
	{
		creditsButton.GetComponentInChildren<Text>().color = Color.red;
	}
	
	public void onDeselectCredits()
	{
		creditsButton.GetComponentInChildren<Text>().color = creditsColor;
	}
}
