using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

	public static ScoreSystem instance;
	public int score = 0;
	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore(int a)
	{
		score += a;
	}
}
