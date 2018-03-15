using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
	struct face
	{
		public int color;
		public Renderer rend;
	};
	public static Player instance;

	private bool controllable;

	private Animator anim;

	private int direction;

	private int steps;

	public float Percentage;

	private face[] faces;

	private int[] faceMapping;

	private Color[] colors;
	// Use this for initialization
	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	void Start ()
	{
		controllable = true;
		anim = GetComponent<Animator>();
		colors = new Color[7] {Color.blue, Color.cyan, Color.yellow, Color.green, Color.red, Color.magenta, Color.clear};
		Renderer[] tmp = GetComponentsInChildren<Renderer>();
		if(tmp.Length!=6) Debug.LogError("Face number not correct!");
		faces = new face[6];
		for (int i = 0; i < 6; i++)
		{
			faces[i].color = i;
			faces[i].rend = tmp[i];
			tmp[i].material.color = colors[i];
		}
		faceMapping=new int[16]{0,4,1,5,0,5,1,4,0,3,1,2,0,2,1,3};
		

		//Time.timeScale = 0.01f;

	}
	
	// Update is called once per frame

	
	void Update () {
		if (controllable)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				
				anim.SetTrigger("UP");
				controllable = false;
				steps = 1;
				direction = 1;
			}
			else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				anim.SetTrigger("DOWN");
				controllable = false;
				steps = 1;
				direction = 2;
				
			}
			else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				anim.SetTrigger("LEFT");
				controllable = false;
				steps = 1;
				direction = 3;
			}
			else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				anim.SetTrigger("RIGHT");
				controllable = false;
				steps = 1;
				direction = 4;
			}
		}
		else
		{
			updatePosition();
		}
	}

	private void goUp()
	{
		Plane.instance.PlayerGoUp(steps);
		updateColor(0);
		transform.position = new Vector3(0,0,-steps);
		Plane.instance.Flush();
	}

	private void goDown()
	{
		Plane.instance.PlayerGoDown(steps);
		updateColor(1);
		transform.position = new Vector3(0,0,steps);
		Plane.instance.Flush();
		
	}
	
	private void goLeft()
	{
		Plane.instance.PlayerGoLeft(steps);
		updateColor(2);
		transform.position = new Vector3(steps,0,0);
		Plane.instance.Flush();
	}
	
	private void goRight()
	{
		Plane.instance.PlayerGoRight(steps);
		updateColor(3);
		transform.position = new Vector3(-steps,0,0);
		Plane.instance.Flush();
	}
	

	private void updateColor(int index)
	{
		int tmp_i = faces[faceMapping[index * 4]].color;
		Color tmp_c = faces[faceMapping[index * 4]].rend.material.color;
		for (int i = 0; i < 3; i++)
		{
			faces[faceMapping[index * 4 + i]].color = faces[faceMapping[index * 4 + i + 1]].color;
			faces[faceMapping[index * 4 + i]].rend.material.color = colors[faces[faceMapping[index * 4 + i + 1]].color];
		}
		faces[faceMapping[(index + 1) * 4 - 1]].color = tmp_i;
		faces[faceMapping[(index + 1) * 4 - 1]].rend.material.color = tmp_c;

	}

	private void updatePosition()
	{
		switch (direction)
		{
			case 0:
			{
				break;
				
			}
			case 1:
			{
				// Up
				transform.position = new Vector3(0,0,-steps * Percentage);
				break;
			}
			case 2:
			{
				// Down
				transform.position = new Vector3(0,0,steps * Percentage);
				break;
			}
			case 3:
			{
				// Left
				transform.position = new Vector3(steps * Percentage, 0, 0);
				break;
			}
			case 4:
			{
				// Right
				transform.position = new Vector3(-steps * Percentage, 0, 0);
				break;
			}		
		}
	}


	public void EnableControl()
	{
		controllable = true;
	}

	public void CheckPosition()
	{
		int center = Plane.instance.GetCenter();
		if (center==6)
		{
			anim.enabled = false;
			controllable = false;
		}
		else
		{
			//if(center!=0) print("center != 0");
			//print("center color = " + center.ToString());
			if(faces[0].color==center) ScoreSystem.instance.AddScore(1);
			transform.position=Vector3.zero;
		}
	}

	
}
