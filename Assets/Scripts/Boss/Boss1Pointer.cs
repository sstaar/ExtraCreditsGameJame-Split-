using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Pointer : MonoBehaviour {

	[SerializeField]
	private float	offset;

	[SerializeField]
	private	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveAround();
	}

	
	void	MoveAround()
	{
		Vector3 mousePos = player.transform.position - transform.position;
		float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Random.Range(offset - 5, offset + 5));  
	}

}
