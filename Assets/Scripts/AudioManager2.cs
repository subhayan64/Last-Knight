using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager2 : MonoBehaviour
{

	public static AudioManager2 instance;
	
	

	//public AudioMixerGroup mixerGroup;

	public Sound2[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound2 s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			//s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	void Start()
	{
		//if(SceneManager.GetActiveScene().buildIndex>0){
		//	Play("theme");
		//}
		
		//Play("theme");

		//if (SceneManager.GetActiveScene().buildIndex == 0)
		//{
		//	Play("introMusic");
		//}
		//else
		//{
		//	Play("theme");
		//}
	}

	void Update()
	{
		

		if (Time.timeScale == 0)
		{
			//Debug.Log("time stopped");

			foreach (Sound2 s in sounds)
			{
				//Debug.Log("sound name: " + s.name);
				if (s.name == "theme")
				{
					continue;
				}
				else
				{
					
					if (s.source.isPlaying)
					{
						StopSound(s.name);
						Debug.Log("sound stopped name: " + s.name);
						
					}
					
				}
				//else
				//{
				//	StopSound(s.name);
				//	Debug.Log("audio stopped");

				//}
			}
		}

	}

	void FixedUpdate()
	{
		
	}
	public void Play(string sound)
	{
		Sound2 s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + s.name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StopSound(string sound)
	{
		Sound2 s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + s.name + " not found!");
			return;
		}

		//StartCoroutine(FadeOut(s.source, 0.5f));


		s.source.Stop();
	}

	public bool isplaying(string sound)
	{
		Sound2 s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + s.name + " not found!");
			return false;
		}



		if (s.source.isPlaying)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

			yield return null;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
	}
}
