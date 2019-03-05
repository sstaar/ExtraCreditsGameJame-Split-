using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {


	[SerializeField]
	private float	offset;

	[SerializeField]
	private	float attackRadius;
	[SerializeField]
	private	Transform	shotPoint;

	[SerializeField]
	private	float	coolDown;
	private	float	coolDownOri;

	[SerializeField]
	private	GameObject player;

	[SerializeField]
	private	Animator iceAnim;

	void	Start()
	{
		player = transform.parent.transform.parent.gameObject;
		coolDownOri = coolDown;
		iceAnim = transform.parent.gameObject.GetComponent<Animator>();
	}

	void	Update()
	{
		Animate();
		if (coolDown > 0)
			coolDown -= Time.deltaTime;
	}

	void	FixedUpdate()
	{
		MoveAround();
		SwordAttacking();
	}

	void	SwordAttacking()
	{
		Collider2D[] enemiesToAttack;
		if (Input.GetMouseButton(0) && coolDown <= 0)
		{
			//Create a circle and attack all enemies in it
			GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(Random.Range(0.01f, 0.03f), 0.2f);
			enemiesToAttack = Physics2D.OverlapCircleAll(shotPoint.position, attackRadius);
			for (int i = 0; i < enemiesToAttack.Length; i++)
			{
				//Checking if the collider in the circle is an enemy
				if (enemiesToAttack[i].gameObject.tag == "Enemy")
				{
					enemiesToAttack[i].gameObject.GetComponent<EnemySystem>().TakeDamage(player.GetComponent<PlayerStats>().strenght);
					player.GetComponent<PlayerStats>().Heal(5f);
				}
				else if (enemiesToAttack[i].gameObject.tag == "Boss1")
				{
					enemiesToAttack[i].gameObject.GetComponent<Boss1>().TakeDamage(player.GetComponent<PlayerStats>().strenght);
					player.GetComponent<PlayerStats>().Heal(player.GetComponent<PlayerStats>().strenght / 10);
				}
				else if (enemiesToAttack[i].gameObject.tag == "Boss2")
				{
					enemiesToAttack[i].gameObject.GetComponent<Boss2>().TakeDamage(player.GetComponent<PlayerStats>().strenght);
					player.GetComponent<PlayerStats>().Heal(player.GetComponent<PlayerStats>().strenght / 10);
				}
			}
			coolDown = coolDownOri;
		}
	}

	void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position just for debug...
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shotPoint.position, attackRadius);
    }

	void	MoveAround()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
		float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);  
	}

	void	Animate() //Animating the Fire Player
	{
		if (player.GetComponent<PlayerMovements>().dir_x == 0 && player.GetComponent<PlayerMovements>().dir_y == 0)
			iceAnim.SetBool("Move", false);
		else
			iceAnim.SetBool("Move", true);

		if (Input.GetMouseButton(0))
			iceAnim.SetBool("Attack", true);
		else
			iceAnim.SetBool("Attack", false);
	}

}


