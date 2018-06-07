using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//Singleton Pattern
	public static GameManager Instance;

	public CanvasGroup sceneMessage;
	public float spawnTime;
	public int scoreToWin = 2000;
	public float zombieMovementSpeed;

	private int score;
	private bool paused = false;
	private Text sceneMessageText;
	private Text gameOverText;
	private Text scoreText;
	private GameObject[] pauseObjects;
	private GameObject[] gameOverObjects;
	private GameObject player;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;


	private void Awake()
	{
		if(Instance != null)
		{
			return;
		}
		Instance = this;

		Time.timeScale = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		spawnTime = 5f;
		zombieMovementSpeed = 4f;
		gameOverObjects = GameObject.FindGameObjectsWithTag("ShowGameOver");
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowPause");
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		sceneMessageText = GameObject.Find("sceneMessageText").GetComponent<Text>();
		scoreText.text = "Get " + scoreToWin + " points to win! \nScore: " + score;
		gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
		HideGameOver();
		HidePaused();
	}

	private void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			Pause();
		}
	}

	public void IncreaseScore(int amount)
	{
		score += amount;
		scoreText.text = "Get " + scoreToWin + " points to win! \n Score: " + score;
		if(score >= scoreToWin)
		{
			GameOver(true);
		}
		if(spawnTime > 0)
		{
			spawnTime -= 0.5f;
		}
		zombieMovementSpeed += 0.1f;
	}

	public void GameOver(bool win)
	{
		if (win)
		{
			gameOverText.text = "You Win!";
		}
		else
		{
			gameOverText.text = "You Suck!";
		}

		ShowGameOver();
		PauseControl();
	}

	public void Pause()
	{
		if (paused)
		{
			HidePaused();
		}
		else
		{
			ShowPaused();
		}

		PauseControl();
	}

	public void Reload()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
	}

	public void PauseControl()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			paused = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
			playerController.enabled = false;
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			paused = false;
			playerController.enabled = true;
		}
	}

	public void ShowPaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
	}

	public void HidePaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
	}

	public void ShowGameOver()
	{
		foreach (GameObject g in gameOverObjects)
		{
			g.SetActive(true);
		}
	}

	public void HideGameOver()
	{
		foreach (GameObject g in gameOverObjects)
		{
			g.SetActive(false);
		}
	}

	public void LoadLevel(string level)
	{
		SceneManager.LoadScene(level, LoadSceneMode.Single);
	}

	private IEnumerator ShowMessageCoroutine(string message, float timeToShow, bool hide)
	{
		sceneMessageText.text = message;
		if (hide)
		{
			float timeShown = 0;
			while (timeShown < timeToShow)
			{
				timeShown += Time.deltaTime;
				yield return null;
			}
			sceneMessageText.text = "";
		}
	}

	public void ShowMessage(string message, float timeToShow = 0, bool hide = true)
	{
		StartCoroutine(ShowMessageCoroutine(message, timeToShow, hide));
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
