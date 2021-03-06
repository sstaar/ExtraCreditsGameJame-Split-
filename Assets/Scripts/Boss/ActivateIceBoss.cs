﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIceBoss : MonoBehaviour {

	[SerializeField]
	private	GameObject canvas;

	[SerializeField]
	private	GameObject boss;

	// Use this for initialization
	void Start () {
		boss = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void	OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			canvas.active = true;
			boss.GetComponent<Boss2>().toAttack =  true;
			Destroy(this.gameObject);
		}
	}	// Use this for initialization

}
