using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Plane.instance.PlayerGoUp(1);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Plane.instance.PlayerGoLeft(1);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Plane.instance.PlayerGoRight(1);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Plane.instance.Initialize();
			
		}
	}
}
