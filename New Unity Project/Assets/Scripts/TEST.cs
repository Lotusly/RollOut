﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			//Plane.instance.Initialize();
			Plane.instance.GradualInitialize();
		}
	}
}
