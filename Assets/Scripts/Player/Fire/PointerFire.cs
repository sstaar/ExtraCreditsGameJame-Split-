using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFire : MonoBehaviour {

	[SerializeField]
	private float	offset;

	[SerializeField]
	private	GameObject	fireBall;
	[SerializeField]
	private	Transform	shotPoint;

	[SerializeField]
	private GameObject fireParticales;

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
		FireBall();
	}

	void FixedUpdate()
	{
		
	}

	void	Animate() //Animating the Fire Player
	{
		if (player.GetComponent<PlayerMovements>().dir_x == 0 &&player.GetComponent<PlayerMovements>().dir_y == 0)
			fireAnim.SetBool("Move", false);
		else
			fireAnim.SetBool("Move", true);

		if (Input.GetMouseButton(0))
			fireAnim.SetBool("Attack", true);
		else
			fireAnim.SetBool("Attack", false);
	}

	IEnumerator	FireParts() // Firing the Fire particales 
	{
		yield return new WaitForSeconds(0.05f);
		GameObject parts = Instantiate(fireParticales, shotPoint.position, transform.rotation);
		Destroy(parts, 0.2f);
	}

	void	FireBall()
	{
        if (Input.GetMouseButton(0))
		{
            if (coolDown <= 0 || Input.GetMouseButtonDown(0))
			{
               // cameraShake.gameObject.  
             //   BeginShake(0.1f, 0.2f);
			 	
				Instantiate(fireBall, shotPoint.position, transform.rotation);
				camera.GetComponent<CameraShake>().Shake(Random.Range(0.01f, 0.03f), 0.2f);
				coolDown = coolDownOri;
				StartCoroutine(FireParts());
			}
            else
				coolDown -= Time.deltaTime;
		}
        else 
            coolDown = coolDownOri;

	}

	void	MoveAround()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
		float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);  
	}

}
