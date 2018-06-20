using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public Slider healthSlider;
	public AudioClip deathClip;
	public AudioClip hurtClip;


	private AudioSource playerAudio;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerMovement;
	private bool isDead;
	private int currentHealth;
	private float damageReductionMod = 1;

	public float DamageReductionMod
	{
		get
		{
			return damageReductionMod;
		}

		set
		{
			damageReductionMod = value;
		}
	}

	private void Start()
	{
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= (int)(amount * damageReductionMod);

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

		playerAudio.clip = deathClip;
		playerAudio.Play();

		playerMovement.enabled = false;

		GameManager.Instance.GameOver(false);
	}

	public int GetCurrentHealth()
	{
		return currentHealth;
	}

	public void AddHealth(int amount)
	{
		currentHealth += amount;
		if(currentHealth > startingHealth)
		{
			currentHealth = startingHealth;
		}
		healthSlider.value = currentHealth;
	}
}
