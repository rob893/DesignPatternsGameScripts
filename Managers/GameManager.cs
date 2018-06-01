using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//Singleton Pattern
	public static GameManager Instance;

	public CanvasGroup sceneMessage;
	public Text text;
	public float spawnTime;
	public float zombieMovementSpeed;

	private int score;
	private Text sceneMessageText;
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
		HideGameOver();
	}

	private void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			PauseControl();
		}
	}

	public void IncreaseScore(int amount)
	{
		score += amount;
		text.text = "Score: " + score;
		if(spawnTime > 0)
		{
			spawnTime -= 0.5f;
		}
		zombieMovementSpeed += 0.1f;
	}

	public void GameOver()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		ShowGameOver();
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
			//ShowPaused();
			Cursor.lockState = CursorLockMode.None;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
			playerController.enabled = false;
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			playerController.enabled = true;
			//HidePaused();
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
			//GameObject.Find("PauseButton").GetComponent<Button>().enabled = false;
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

	public IEnumerator ShowMessageCoroutine(string message, float timeToShow)
	{
		sceneMessage.alpha = 1;
		sceneMessageText.text = message;
		float timeShown = 0;
		while (timeShown < timeToShow)
		{
			timeShown += Time.deltaTime;
			yield return null;
		}
		sceneMessage.alpha = 0;
		sceneMessageText.text = "";
	}

	public void ShowMessage(string message, float timeToShow)
	{
		StartCoroutine(ShowMessageCoroutine(message, timeToShow));
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
