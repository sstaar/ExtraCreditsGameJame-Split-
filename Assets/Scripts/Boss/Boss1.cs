using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour {

	[SerializeField]
	private	Image healthBar;

	[SerializeField]
	private	GameObject[] shotPoints;

	[SerializeField]
	private	float coolDown;
	private	float coolDownOri;

	[SerializeField]
	private	float maxHealth;
	private float health;

	[SerializeField]
	private	GameObject attack;

	public	bool toAttack;

	private Animator anim;

	[SerializeField]
	private	GameObject nextlevel;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		health = maxHealth;
		coolDownOri = coolDown;
		toAttack = false;
	}
	

	// Update is called once per frame
	void Update () {
		Attacking();
		if (health <= 0)
		{
			nextlevel.active = true;
			Destroy(this.gameObject);
		}
		healthBar.fillAmount = health / maxHealth;
	}

	IEnumerator	animat()
	{
		anim.SetBool("Attack", true);
		yield return new WaitForSeconds(1.5f);
		anim.SetBool("Attack", false);
	}

	void	Attacking()
	{
		if (toAttack && coolDown <= 0)
		{
			
			Fire();
			coolDown = coolDownOri;
			StartCoroutine(animat());
		}
		else
			coolDown -= Time.deltaTime;
	}

	void	Fire()
	{	
		for (int i = 0; i < shotPoints.Length; i++)
			Instantiate(attack, shotPoints[i].transform.position, shotPoints[i].transform.rotation);
	}

	public	void	TakeDamage(float damage)
	{
		health -= damage;
	}

}
