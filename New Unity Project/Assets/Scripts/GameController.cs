using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	public float possibility;

	private float timeStart;

	public float Ingrediant;

	public GameObject Canvas;
	// Use this for initialization

	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start ()
	{
		//Time.timeScale = 0.1f;
		timeStart = Time.time;
		possibility = 0.02f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		possibility = 0.02f + Ingrediant * Time.time - timeStart;
	}

	private void OnTriggerEnter(Collider other)
	{
		possibility = 0;
		Ingrediant = 0;
		Canvas.active = true;
		Text [] tmp=Canvas.GetComponentsInChildren<Text>();
		tmp[0].text = "Score: " + ScoreSystem.instance.score;
		tmp[1].text = "Time: " + (Time.time - timeStart).ToString() + "s";
	}
}
