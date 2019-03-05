using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour {

	[SerializeField]
	private	Image healthBar;

	[SerializeField]
	private	GameObject[] shotPoints;

	[SerializeField]
	private	float attackCoolDown;
	private	float attackCoolDownOri;

	[SerializeField]
	private	float stanceCoolDown;

	private	float stanceCoolDownOri;

	[SerializeField]
	private	float maxHealth;
	private float health;

	[SerializeField]
	private	GameObject attack;

	public	bool toAttack;

	private Animator anim;

	[SerializeField]
	private	GameObject nextlevel;

	[SerializeField]
	private	bool defStance;

	[SerializeField]
	private	float healPower;

	[SerializeField]
	private	float spikesRadius;

	[SerializeField]
	private	GameObject[] spikes;

	// Use this for initialization
	void Start () {
		defStance = false;
		anim = GetComponent<Animator>();
		health = maxHealth;
		attackCoolDownOri = attackCoolDown;
		stanceCoolDownOri = stanceCoolDown;
		toAttack = false;
	}

	void	StanceManager()
	{
		if (stanceCoolDown <= 0 )
		{
			if (defStance)
			{
				defStance = false;
				anim.SetBool("StartDef", false);
			}
			else
			{
				anim.SetBool("StartDef", true);
				defStance = true;
			}
			stanceCoolDown = stanceCoolDownOri;
		}
		else
		{
			stanceCoolDown -= Time.deltaTime;
		}

		if (defStance)
		{
			health += Time.deltaTime * healPower;
			for (int i = 0; i < spikes.Length; i++)
			{
				Collider2D[] players = Physics2D.OverlapCircleAll(spikes[i].transform.position, spikesRadius);
				for (int j = 0; j < players.Length; j++)
				{
					if (players[j].gameObject.tag == "Player")
						players[j].gameObject.GetComponent<PlayerStats>().TakeDamage(0.5f);
				}
			}
		}
	}

	void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position just for debug...
        Gizmos.color = Color.red;
		for (int i = 0; i < spikes.Length; i++)
			Gizmos.DrawWireSphere(spikes[i].transform.position, spikesRadius);
    
	}

	// Update is called once per frame
	void Update () {
		
		StanceManager();
		if (!defStance)
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
		if (toAttack && attackCoolDown <= 0)
		{
			
			Fire();
			attackCoolDown = attackCoolDownOri;
			StartCoroutine(animat());
		}
		else
			attackCoolDown -= Time.deltaTime;
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
