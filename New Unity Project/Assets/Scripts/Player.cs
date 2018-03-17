using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
	private int nextDirection;

	private int steps;

	private int nextSteps;
	
	//public float Percentage;

	private face[] faces;

	private int[] faceMapping;

	private Color[] colors;

	//private Coroutine running;
	public bool update=false ;
	private AnimatorStateInfo animationState;
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

	
	void Update ()
	{
		if (update) updatePosition();
		if (controllable)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				anim.SetTrigger("UP");
				controllable = false;
				nextDirection = 1;

				if (Input.GetKey(KeyCode.Space))
				{
					if(ScoreSystem.instance.AddEnergy(-4))
						nextSteps = 2;
				}
				else
				{
					nextSteps = 1;
				}
			}
			else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				anim.SetTrigger("DOWN");
				controllable = false;
				
				nextDirection = 2;

				if (Input.GetKey(KeyCode.Space))
				{
					if(ScoreSystem.instance.AddEnergy(-4))
						nextSteps = 2;

				}
				else
				{
					nextSteps = 1;
				}
				
			}
			else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				anim.SetTrigger("LEFT");
				controllable = false;
				nextDirection = 3;
				if (Input.GetKey(KeyCode.Space))
				{
					if(ScoreSystem.instance.AddEnergy(-4))
						nextSteps = 2;			
				}
				else
				{
					nextSteps = 1;
				}
			}
			else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				anim.SetTrigger("RIGHT");
				controllable = false;
				nextDirection = 4;
				if (Input.GetKey(KeyCode.Space))
				{
					if(ScoreSystem.instance.AddEnergy(-4))
						nextSteps = 2;
				}
				else
				{
					nextSteps = 1;
				}
			}
			else if (Input.GetKeyDown(KeyCode.Return))
			{
				if(ScoreSystem.instance.AddEnergy(-8))
					Plane.instance.Initialize();
				
			}
		}
		else
		{
			//updatePosition();
		}
	}

	private void goUp()
	{
		
		direction = nextDirection;
		steps = nextSteps;
		print("up "+steps);
		Plane.instance.PlayerGoUp(steps);
		updateColor(0);
		//transform.position = new Vector3(0,0,-steps);
		updatePosition();
		Plane.instance.Flush();
		
		//update = true;
	}

	private void goDown()
	{
		//print("down");
		direction = nextDirection;
		steps = nextSteps;
		print("down "+steps);
		Plane.instance.PlayerGoDown(steps);
		updateColor(1);
		//transform.position = new Vector3(0,0,steps);
		updatePosition();
		Plane.instance.Flush();
		//animationState = anim.GetCurrentAnimatorStateInfo(0);
		//update = true;
	}
	
	private void goLeft()
	{
		//print("left");
		direction = nextDirection;
		steps = nextSteps;
		print("left "+steps);
		Plane.instance.PlayerGoLeft(steps);
		updateColor(2);
		//transform.position = new Vector3(steps,0,0);
		updatePosition();
		Plane.instance.Flush();
		//animationState = anim.GetCurrentAnimatorStateInfo(0);
		//update = true;
	}
	
	private void goRight()
	{
		//print("right");
		direction = nextDirection;
		steps = nextSteps;
		print("right "+steps);
		Plane.instance.PlayerGoRight(steps);
		updateColor(3);
		//transform.position = new Vector3(-steps,0,0);
		updatePosition();
		Plane.instance.Flush();
		//animationState = anim.GetCurrentAnimatorStateInfo(0);
		//update = true;
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
		//yield return null;
		
		switch (direction)
		{
			case 1:
			{
				// Up
				animationState = anim.GetCurrentAnimatorStateInfo(0);
				transform.position = new Vector3(0, 0, -steps * (1-animationState.normalizedTime));
					//yield return new WaitForEndOfFrame();
				
				break;
			}
			case 2:
			{
				// Down
				animationState = anim.GetCurrentAnimatorStateInfo(0);
				transform.position = new Vector3(0, 0, steps * (1-animationState.normalizedTime));
					//yield return new WaitForEndOfFrame();
				
				break;
			}
			case 3:
			{
				// Left
				animationState = anim.GetCurrentAnimatorStateInfo(0);
				transform.position = new Vector3(steps * (1-animationState.normalizedTime), 0, 0);
					//yield return new WaitForEndOfFrame();
				
				break;
			}
			case 4:
			{
				// Right
				animationState = anim.GetCurrentAnimatorStateInfo(0);
				transform.position = new Vector3(-steps * (1-animationState.normalizedTime), 0, 0);
					//yield return new WaitForEndOfFrame();
				
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
		//update = false;
		int center = Plane.instance.GetCenter();
		if (center==6)
		{
			anim.enabled = false;
			update = false;
			controllable = false;
		}
		else
		{
			//if(center!=0) print("center != 0");
			//print("center color = " + center.ToString());
			if (faces[0].color == center)
			{
				Plane.instance.HitCenter();
				ScoreSystem.instance.AddScore(1);
				ScoreSystem.instance.AddEnergy(1);
			}
			transform.position=Vector3.zero;
		}
	}

	
}
