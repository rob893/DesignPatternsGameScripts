using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour {

	public float Lifetime = 2;

	private ParticleSystem system;
	private AudioSource soundEffect;

	
	private void Awake()
	{
	   system = GetComponent<ParticleSystem>();
		if (GetComponent<AudioSource>())
		{
			soundEffect = GetComponent<AudioSource>();
		}
	}

	private void OnEnable ()
    {
		if(soundEffect != null)
		{
			soundEffect.Play();
		}
       StartCoroutine(ParticleCoroutine(Lifetime, system));
    }

    public IEnumerator ParticleCoroutine(float duration, ParticleSystem effect)
    {
        yield return new WaitForSeconds(duration);
        effect.Stop();
        gameObject.SetActive(false);
    }


}
