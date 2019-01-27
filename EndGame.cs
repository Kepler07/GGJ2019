using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Button replayButton;
    public Button menuButton;
    private AudioSource audioSource;
    public string playScene = "MainLevel";
    public string menuScene = "menu_new";
    public Font menuFont;
    public AudioClip menuClip;
    public AudioClip playClip;

    // Use this for initialization
    private void Start()
    {
        replayButton.onClick.AddListener(replayGame);
        menuButton.onClick.AddListener(gotoMenu);

        replayButton.GetComponentInChildren<Text>().font = menuFont;
        menuButton.GetComponentInChildren<Text>().font = menuFont;

        GameObject go = GameObject.Find("multisceneaudio");
        if (go != null)
        {
            audioSource = go.GetComponent<AudioSource>();
        }

        //DontDestroyOnLoad(audioSource);
    }


    public void replayGame()
    {
        Invoke("loadSceneGame", 1);
    }

    private void gotoMenu()
    {
        audioSource.clip = menuClip;
        audioSource.Play();
        Invoke("loadSceneMenu", 1);
    }

    private void loadSceneGame()
    {
        SceneManager.LoadScene(playScene, LoadSceneMode.Single);
    }

    private void loadSceneMenu()
    {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
    }
}