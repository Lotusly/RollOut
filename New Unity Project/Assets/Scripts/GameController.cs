using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	public float possibility;

	private float timeStart;

	public float Ingrediant;
	// Use this for initialization

	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start ()
	{
		timeStart = Time.time;
		possibility = 0.02f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		possibility = 0.02f + Ingrediant * Time.time - timeStart;
	}
}
