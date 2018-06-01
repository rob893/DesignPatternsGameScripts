using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	//public Image damageImage;
	public AudioClip deathClip;
	public AudioClip hurtClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


	//Animator anim;
	AudioSource playerAudio;
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerMovement;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;


	void Awake()
	{
		//anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		playerShooting = GetComponentInChildren<PlayerShooting>();
		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
	}


	void Update()
	{
		if (damaged)
		{
			//damageImage.color = flashColour;
		}
		else
		{
			//damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}


	public void TakeDamage(int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;
		playerAudio.clip = hurtClip;
		playerAudio.Play();

		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}


	void Death()
	{
		isDead = true;

		playerShooting.DisableEffects();

		//anim.SetTrigger("Die");

		playerAudio.clip = deathClip;
		playerAudio.Play();
		playerMovement.m_MouseLook.lockCursor = false;
		playerMovement.enabled = false;
		playerShooting.enabled = false;
		GameManager.Instance.GameOver();
	}


	public void RestartLevel()
	{
		SceneManager.LoadScene(0);
	}
}
