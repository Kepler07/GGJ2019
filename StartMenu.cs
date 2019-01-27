using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

	public Button startButton;
	public Button quitButton;
	public AudioSource audioSource;
	public string playScene = "MainLevel";
	public Font menuFont;
	public AudioClip menuClip;
	public AudioClip playClip;
	
	// Use this for initialization
	private void Start () {
		startButton.onClick.AddListener(startGame);
		quitButton.onClick.AddListener(quitGame);

		startButton.GetComponentInChildren<Text>().font = menuFont;
		
		GameObject go = GameObject.Find("multisceneaudio");
		if (go != null)
		{
			audioSource = go.GetComponent<AudioSource>();
		}
		
		audioSource.clip = menuClip;
		audioSource.Play();
		audioSource.name = "multisceneaudio";
		DontDestroyOnLoad(audioSource);
	}


	public void startGame()
	{
		Debug.Log("Start clicked");

		audioSource.clip = playClip;
		audioSource.Play();
		Invoke("loadSceneGame", 1);

}
	
	private void quitGame()
	{
		Debug.Log("Quit clicked");
		Application.Quit();
	}
	
	private void loadSceneGame()
	{
		SceneManager.LoadScene(playScene, LoadSceneMode.Single);
	}


}
