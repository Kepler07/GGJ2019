using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

	public Button startButton;
	public Button quitButton;
	public Button creditsButton;
	public AudioSource audioSource;
	public string playScene = "MainLevel";
	public string creditsScene = "creditsScene";
	
	// Use this for initialization
	private void Start () {
		startButton.onClick.AddListener(startGame);
		quitButton.onClick.AddListener(quitGame);
		creditsButton.onClick.AddListener(creditsGame);
	}


	public void startGame()
	{
		Debug.Log("Start clicked");
		audioSource.Play();
		Invoke("loadSceneGame", 1);
	}

	public void creditsGame()
	{
		Debug.Log("Credits clicked");
		audioSource.Play();
		Invoke("loadSceneCredit", 1);
	}
	
	private void loadSceneGame()
	{
		SceneManager.LoadScene(playScene, LoadSceneMode.Single);
	}

	private void loadSceneCredit(){
		SceneManager.LoadScene(creditsScene, LoadSceneMode.Single);
	}

	private void quitGame()
	{
		Debug.Log("Quit clicked");
		Application.Quit();
	}
	
	
}
