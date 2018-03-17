using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	[Serializable] private struct sound
	{
		public string name;
		public AudioClip audio;
	};

	
	[SerializeField] private sound[] sounds;
	[SerializeField] private sound[] musics;
	public Dictionary<string, AudioClip> soundDictionary;
	public Dictionary<string, AudioClip> musicDictionary;
	public static SoundController instance;
	private AudioSource [] sources;

	//public AudioClip[] Musics;

	//public AudioClip[] Sounds;

	public Coroutine inProcess;
	// Use this for initialization
	
	//-------------TEST---------------
	public float SpeedMergeMusic;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null) instance = this;
	}
	void Start ()
	{
		sources = GetComponentsInChildren<AudioSource>();
		soundDictionary = new Dictionary<string, AudioClip>();
		musicDictionary=new Dictionary<string, AudioClip>();
		for (int i = 0; i < sounds.Length; i++)
		{
			soundDictionary.Add(sounds[i].name, sounds[i].audio);
		}
		for (int i = 0; i < musics.Length; i++)
		{
			musicDictionary.Add(musics[i].name, musics[i].audio);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*public void EnterQuestion()
	{
		if (inProcess != null)
		{
			StopCoroutine(inProcess);
		}
		inProcess = StartCoroutine(switchMusic(0));
	}*/


	public void SwitchMusic(string name)
	{
		if (inProcess != null)
		{
			StopCoroutine(inProcess);
		}
		inProcess = StartCoroutine(switchMusic(name));
	}

	public void PlaySound(string name)
	{
		if (soundDictionary.ContainsKey(name))
		{
			sources[1].clip = soundDictionary[name];
			sources[1].Play();
		}
	}

	

	private IEnumerator switchMusic(string name)
	{
		yield return null;
		if (musicDictionary.ContainsKey(name))
		{
			while (sources[0].volume > 0)
			{
				sources[0].volume -= SpeedMergeMusic * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			
				
			sources[0].clip = musicDictionary[name];
			
			sources[0].volume = 1;
			sources[0].Play();

		}
		else if(name=="")
		{
			while (sources[0].volume > 0)
			{
				sources[0].volume -= SpeedMergeMusic * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			sources[0].volume = 0;
			sources[0].clip = null;
		}
		//inProcess = null;
	}
}
