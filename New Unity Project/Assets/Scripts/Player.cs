using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;

	private bool controllable;

	private Animator anim;

	private int direction;

	private int steps;

	public float Percentage;

	private Renderer[] faces;

	private int[] faceMapping;
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
		faces = GetComponentsInChildren<Renderer>();
		if(faces.Length!=6) Debug.LogError("Face number not correct!");
		faceMapping=new int[16]{0,4,1,5,0,5,1,4,0,3,1,2,0,2,1,3};

		//Time.timeScale = 0.01f;

	}
	
	// Update is called once per frame

	
	void Update () {
		if (controllable)
		{
			if (Input.GetKeyDown(KeyCode.W))
			{
				
				anim.SetTrigger("UP");
				controllable = false;
				steps = 1;
				direction = 1;
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				anim.SetTrigger("DOWN");
				controllable = false;
				steps = 1;
				direction = 2;
				
			}
			else if (Input.GetKeyDown(KeyCode.A))
			{
				anim.SetTrigger("LEFT");
				controllable = false;
				steps = 1;
				direction = 3;
			}
			else if (Input.GetKeyDown(KeyCode.D))
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
		Plane.instance.PlayerGoUp(1);
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
		Plane.instance.PlayerGoLeft(1);
		updateColor(2);
		transform.position = new Vector3(steps,0,0);
		Plane.instance.Flush();
	}
	
	private void goRight()
	{
		Plane.instance.PlayerGoRight(1);
		updateColor(3);
		transform.position = new Vector3(-steps,0,0);
		Plane.instance.Flush();
	}
	

	private void updateColor(int index)
	{
		Color tmp = faces[faceMapping[index * 4]].material.color;
		for (int i = 0; i < 3; i++)
		{
			faces[faceMapping[index * 4 + i]].material.color = faces[faceMapping[index * 4 + i + 1]].material.color;
		}
		faces[faceMapping[(index + 1) * 4 - 1]].material.color = tmp;

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

		if (!Plane.instance.CheckCenter())
		{
			anim.enabled = false;
			controllable = false;
		}
		else
		{
			transform.position=Vector3.zero;
		}
	}

	
}
