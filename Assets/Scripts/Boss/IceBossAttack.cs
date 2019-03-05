using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossAttack : MonoBehaviour {

	[SerializeField]
	private	float speed;

	[SerializeField]
	private	float damage;

	[SerializeField]
	private	float lifeTime;
	[SerializeField]
	private GameObject	player;

	

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, lifeTime);
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Rigidbody2D>().AddRelativeForce( -Vector2.up * speed);
		FollowPlayer();
	}

	void	FollowPlayer()
	{
		GetComponent<Rigidbody2D>().velocity = (player.transform.position - this.transform.position) * speed;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
