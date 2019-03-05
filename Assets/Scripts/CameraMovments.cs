using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovments : MonoBehaviour {

	[SerializeField]
	private	GameObject player;


	[SerializeField]
	private	float	lerpval;

	// Use this for initialization
	void Start () {

	}

	void	Update()
	{
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FollowPlayer();
	}

	void	FollowPlayer()
	{
		float	x;
		float	y;

		x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, lerpval);
		y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, lerpval);
		this.transform.position = new Vector3(x, y, this.transform.position.z);
	}
}
