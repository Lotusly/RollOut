using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

	public static ScoreSystem instance;
	public int score = 0;
	public int energy = 0;
	public Slider SkillBar;
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

	public bool AddEnergy(int a)
	{
		if (a > 0)
		{
			energy = Mathf.Min(energy + a, 8);
			SkillBar.value=energy*0.125f;
			return true;
		}
		else
		{
			if (energy + a >= 0)
			{
				energy = energy + a;
				SkillBar.value=energy*0.125f;
				return true;
			}
			else
			{
				return false;
			}
		}
		
		//energy = Mathf.Max(0,Mathf.Min(energy + a, 8));
		//SkillBar.value=energy*0.125f;
	}
}
