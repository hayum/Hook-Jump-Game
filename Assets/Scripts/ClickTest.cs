﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")){
			anim.SetBool("Jump",true);
		}
		else{
			anim.SetBool("Jump",false);
		}
	}
}
