using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject helpMenu;
	public GameObject loadingScreen;
	public Slider slider;

	private void Awake()
	{
		mainMenu.SetActive(true);
		helpMenu.SetActive(false);
		loadingScreen.SetActive(false);
	}

	public void PlayGame(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	private IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingScreen.SetActive(true);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progress;

			yield return null;
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ShowHelp()
	{
		mainMenu.SetActive(false);
		helpMenu.SetActive(true);
	}

	public void ShowMainMenu()
	{
		mainMenu.SetActive(true);
		helpMenu.SetActive(false);
	}
}
