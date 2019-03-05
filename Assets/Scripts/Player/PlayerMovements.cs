using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

	//[SerializeField]
	//private	Animator anim;

	[SerializeField]
	private	float speed;

	public	int	dir_x;
	public	int	dir_y;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		this.transform.position = Vector3.zero;
		rb = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move();
		//AttackAnimation();
	}

	void	Move()
	{
		if (Input.GetAxisRaw("Horizontal") > 0)
			dir_x = 1;
		else if (Input.GetAxisRaw("Horizontal") < 0)
			dir_x = -1;
		else
			dir_x = 0;
		
		if (Input.GetAxisRaw("Vertical") > 0)
			dir_y = 1;
		else if (Input.GetAxisRaw("Vertical") < 0)
			dir_y = -1;
		else
			dir_y = 0;

		//This part is for the Move animation
		/*if (dir_y == 0 && dir_x == 0)
			anim.SetBool("Move", false);
		else
			anim.SetBool("Move", true);*/
		//rb.AddForce(new Vector2(dir_x * speed, dir_y * speed));
		rb.velocity = new Vector2(dir_x * speed, dir_y * speed);
	}

	/*void	AttackAnimation()
	{
		//This part is for the Attack animation
		if (Input.GetMouseButton(0))
			anim.SetBool("Attack", true);
		else
			anim.SetBool("Attack", false);
	}*/

}
