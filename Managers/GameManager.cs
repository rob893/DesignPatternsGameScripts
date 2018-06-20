using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

	//Singleton Pattern
	public static GameManager Instance;

	public float spawnTime = 5;
	public int scoreToWin = 2000;
	public GameObject pauseMenuUI;
	public bool gameIsPaused = false;
	public bool gameIsOver = false;

	private int score = 0;
	private GameObject crosshair;
	private GameObject resumeButtom;
	private TextMeshProUGUI sceneMessageText;
	private TextMeshProUGUI pauseText;
	private TextMeshProUGUI scoreText;
	private GameObject player;
	private WeaponManager weaponManager;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;


	//Singleton
	private GameManager() { }

	private void Awake()
	{
		//enforce singleton
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(this);
		}

		resumeButtom = GameObject.Find("ResumeButton");
		crosshair = GameObject.Find("Crosshair");
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		weaponManager = player.GetComponentInChildren<WeaponManager>();
		scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
		sceneMessageText = GameObject.Find("sceneMessageText").GetComponent<TextMeshProUGUI>();
		scoreText.text = "Get " + scoreToWin + " points to win! \nScore: " + score;
		pauseText = GameObject.Find("PauseText").GetComponent<TextMeshProUGUI>();
		pauseText.text = "PAUSED";
		Resume();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused && !gameIsOver)
			{
				Resume();
			}
			else
			{
				Pause();
			}
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
	}

	public void GameOver(bool win)
	{
		gameIsOver = true;

		if (win)
		{
			pauseText.text = "YOU WIN!";
		}
		else
		{
			pauseText.text = "YOU SUCK!";
		}
		pauseText.transform.position = pauseText.transform.position - new Vector3(0, 50, 0);
		Pause();
		resumeButtom.SetActive(false);
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		playerController.enabled = false;
		weaponManager.enabled = false;
		crosshair.SetActive(false);
		sceneMessageText.text = "";
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		playerController.enabled = true;
		weaponManager.enabled = true;
		crosshair.SetActive(true);
	}

	public void Reload()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
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

	public void SetLayer(GameObject obj, int layer)
	{
		obj.layer = layer;
		foreach (Transform child in obj.transform)
			SetLayer(child.gameObject, layer);
	}

	public int Score
	{
		get
		{
			return score;
		}

		set
		{
			score = value;
		}
	}
}
