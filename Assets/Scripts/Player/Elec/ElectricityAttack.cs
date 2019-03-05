using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityAttack : MonoBehaviour {

	[SerializeField]
	private float	offset;

	[SerializeField]
	private	Transform	shotPoint;

	[SerializeField]
	private GameObject elecParticales;

	[SerializeField]
	private	float	coolDown;
    [SerializeField]
	private	float	coolDownOri;

	[SerializeField]
	private	Animator	fireAnim;

	[SerializeField]
	private	GameObject player;

	[SerializeField]
	private	GameObject	camera;

	[SerializeField]
	private	float	attackRadius;

	[SerializeField]
	private	GameObject mouseCors;


	void	Start()
	{
        //cameraShake = GameObject.FindGameObjectWithTag("Pointer").GetComponent<CameraShake>();
        //cameraShake = objectWithScript.GetComponent<CameraShake>();
        //cameraShake = GetComponent<CameraShake>();
        fireAnim = transform.parent.GetComponent<Animator>();
		coolDownOri = coolDown;
		player = transform.parent.gameObject.transform.parent.gameObject;
		camera = GameObject.Find("Main Camera");
	}

	void	Update()
	{
		Animate();
		MoveAround();
	}

	void FixedUpdate()
	{
		Attack();
	}



	void	Attack()
	{
		if (coolDown <= 0 && Input.GetMouseButtonDown(0))
		{
			GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(Random.Range(0.01f, 0.03f), 0.2f);
			GameObject temp = Instantiate(elecParticales, mouseCors.transform.position, mouseCors.transform.rotation);
			Destroy(temp, 1f);
			Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(mouseCors.transform.position, attackRadius);
			for (int i = 0; i < enemiesInRange.Length; i++)
			{
				if (enemiesInRange[i].gameObject.tag == "Enemy")
				{
					enemiesInRange[i].gameObject.GetComponent<EnemySystem>().TakeDamage(player.GetComponent<PlayerStats>().strenght / 3);
					coolDown = coolDownOri;
				}
				else if (enemiesInRange[i].gameObject.tag == "Boss1")
				{
					enemiesInRange[i].gameObject.GetComponent<Boss1>().TakeDamage(player.GetComponent<PlayerStats>().strenght / 3);
					coolDown = coolDownOri;
				}
				else if (enemiesInRange[i].gameObject.tag == "Boss2")
				{
					enemiesInRange[i].gameObject.GetComponent<Boss2>().TakeDamage(player.GetComponent<PlayerStats>().strenght / 3);
					coolDown = coolDownOri;
				}
			}
		}
		else
			coolDown -= Time.deltaTime;
	}

	void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position just for debug...
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mouseCors.transform.position, attackRadius);
    }

	void	Animate() //Animating the Fire Player
	{
		if (player.GetComponent<PlayerMovements>().dir_x == 0 &&player.GetComponent<PlayerMovements>().dir_y == 0)
			fireAnim.SetBool("Move", false);
		else
			fireAnim.SetBool("Move", true);

		if (Input.GetMouseButtonDown(0))
			fireAnim.SetBool("Attack", true);
		else
			fireAnim.SetBool("Attack", false);
	}

	void	MoveAround()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
		float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);  
	}
}
