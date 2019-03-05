using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossAttack : MonoBehaviour {

	[SerializeField]
	private	float speed;

	[SerializeField]
	private	float damage;

	[SerializeField]
	private	float lifeTime;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().AddRelativeForce( -Vector2.up * speed);
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
