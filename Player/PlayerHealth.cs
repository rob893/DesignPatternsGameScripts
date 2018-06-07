using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public AudioClip deathClip;
	public AudioClip hurtClip;


	private AudioSource playerAudio;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerMovement;
	//private PlayerShooting playerShooting;
	private bool isDead;


	private void Start()
	{
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		//playerShooting = GetComponentInChildren<PlayerShooting>();
		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;

		healthSlider.value = currentHealth;
		playerAudio.clip = hurtClip;
		playerAudio.Play();

		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}

	private void Death()
	{
		isDead = true;

		//playerShooting.DisableEffects();

		playerAudio.clip = deathClip;
		playerAudio.Play();

		playerMovement.enabled = false;
		//playerShooting.enabled = false;

		GameManager.Instance.GameOver(false);
	}
}
