using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plate : MonoBehaviour
{

	private int line;

	private int column;

	private Renderer rend;

	public int state;

	private float possibility;

	private Collider coll;

	public int color;

	private Color[] colors;

	private Coroutine flushing;

	private int colorBuffer;
	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<Renderer>();
		state = 0;
		possibility = 0.02f;
		//colors = new Color[7] {Color.blue, Color.cyan, Color.red, Color.magenta, Color.green, Color.yellow,Color.clear};
		colors = new Color[8] {Color.blue, Color.cyan, Color.yellow, Color.green, Color.red, Color.magenta, Color.clear,Color.white};

		coll = GetComponent<Collider>();
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (state)
		{
			case 0:
			{
				if (color<6 && Random.value< GameController.instance.possibility * Time.deltaTime )
				{
					int tmp = Random.Range(0, 7);
					if (tmp != color)
					{
						state = 1;
						colorBuffer = tmp;
						flushing=StartCoroutine(shining());
					}

				}
				break;
			}
			
		}
	}

	public void AssignColor(int index)
	{
		if (flushing != null)
		{
			StopCoroutine(flushing);
		}
		rend.material.color = colors[index];
		color = index;
		if (index == 6)
		{
			
			coll.enabled = false;
			state = 2;
			Player.instance.CheckPosition();
		}
		else
		{
			coll.enabled = true;
			state = 0;
		}
	}

	public void AssignIndex(int i, int j)
	{
		line = i;
		column = j;
	}

	private IEnumerator shining()
	{
		yield return null;
		bool direction = false;
		int times = 0;
		while (times < 3)
		{
			Color tmp = rend.material.color;
			rend.material.color = new Color(tmp.r,tmp.g,tmp.b,tmp.a*( 1 + (direction ? 0.1f : -0.1f)));
			float a = rend.material.color.a;
			if (a < 0.4f || a > 1)
			{
				direction = !direction;
				if (!direction) times++;
			}
			yield return new WaitForSecondsRealtime(0.02f);
		}
		color = colorBuffer;
		AssignColor(color);
		

	}

	public int GetColor()
	{
		return color;
	}

	
}
