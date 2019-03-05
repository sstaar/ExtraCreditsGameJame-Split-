using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    [SerializeField]
    private bool isAgro;
    [SerializeField]
    private int direction;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    public float agroSpeed;
    private Transform player;
    [SerializeField]
    private float stopingDistance;
    [SerializeField]
    private float agroDistance;

    private Vector3 origin;

    [SerializeField]
    private float   patrolRange;

    [SerializeField]
    private float   patrolCoolDown;
    private float   patrolTime;
    Vector2 movePos = Vector2.zero;

    [SerializeField]
    private Animator enemyAnim;




	void Start () {
        isAgro = false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();       
        origin = transform.position;
        patrolTime = patrolCoolDown;
        enemyAnim = GetComponent<Animator>();
        //direction = Random.Range(1, 8);
	}
	
	// Update is called once per frame
	void Update () {

        //In here i just seperated the part where he know when to follow the play and following the player
        float Distance = Vector2.Distance(transform.position, player.position);

        if (Distance <= agroDistance && Distance > stopingDistance)
            isAgro = true;
        else
            isAgro = false;
        
        if (isAgro)
        {
            enemyAnim.SetBool("Attack", false);
            rb.velocity = (player.position - this.transform.position) * agroSpeed;
        }
        else if (Distance > stopingDistance)
        {
            Patrol();
            enemyAnim.SetBool("Attack", false);
        }
        else if (Distance < stopingDistance)
            enemyAnim.SetBool("Attack", true);
        else
        {
            enemyAnim.SetBool("Attack", false);
            rb.velocity = Vector2.zero;
        }

	}

    void    Patrol()
    {

        if (patrolCoolDown >= 0)
        {
            //Make sure the enemy doesn't move too far away from its starting position
            if (transform.position.x > origin.x + patrolRange)
                movePos.x *= -1;
            else if (transform.position.x < origin.x - patrolRange)
                movePos.x *= -1;

            if (transform.position.y > origin.y + patrolRange)
                movePos.y *= -1;
            else if (transform.position.y < origin.y - patrolRange)
                movePos.y *= -1;
            
            rb.velocity = movePos * speed;
            patrolCoolDown -= Time.deltaTime;
        }
        else
        {
            movePos = new Vector2  (Random.RandomRange(-0.5f, 0.5f), Random.RandomRange(-0.5f, 0.5f)); // Make every move a little random
            rb.velocity = Vector2.zero;
            patrolCoolDown = patrolTime; //Reseting the patrol cooldown
        }
    }

	/*void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Borders")
        {
            agroSpeed = -agroSpeed;                                             //reverse directions if it hit walls
            //direction = Random.Range(1, 8);
        }
	}*/
}
