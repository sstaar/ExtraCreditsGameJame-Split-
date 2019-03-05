using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private int maxMonsters;

	[SerializeField]
	private float	spawningRadius;
	[SerializeField]
	private	float	timeBetweenSpawns;
	[SerializeField]
	private	GameObject enemy;

	private	float coolDown = 0;

	// Use this for initialization
	void Start () {
		
	}

	void	FixedUpdate()
	{
		SpawnSystem();
	}
	
	// Update is called once per frame
	void Update () {
		if (coolDown > 0)
			coolDown -= Time.deltaTime;
	}

	void	SpawnSystem()
	{
		int	enemiesCount = 0;
		Collider2D[] area = Physics2D.OverlapCircleAll(this.transform.position, spawningRadius);
		for (int i = 0; i < area.Length; i++)
		{
			if (area[i].gameObject.tag == "Enemy")
				enemiesCount++;
		}
		if (enemiesCount < maxMonsters && coolDown <= 0)
			SpawnEnemy();
	}

	void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position just for debug...
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, spawningRadius);
    }

	void	SpawnEnemy()
	{
		GameObject temp =Instantiate(enemy, this.transform.position, this.transform.rotation);
		//EnemySystem enemysys = temp.GetComponent<EnemySystem>();
		coolDown = timeBetweenSpawns;
		//enemysys.maxHP = Random.Range(15f,25f);
		//temp.transform.localScale = new Vector3(0.3f * enemysys.maxHP / 20f, 0.3f * enemysys.maxHP / 20f, 0f);
		//temp.GetComponent<EnemyMove>().agroSpeed = 3 - (enemysys.maxHP / 20f) * 2;
	}

}
