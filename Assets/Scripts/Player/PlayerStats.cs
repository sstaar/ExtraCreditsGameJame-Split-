using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {


    [SerializeField]
    private GameObject playerType;

    [SerializeField]
	private	Image healthBar;

    [SerializeField]
    private Image timeBar;

	[SerializeField]
	private	float	lerpSpeed;

	[SerializeField]
	private	float maxHP;
    [SerializeField]
    public float currHP;

    [SerializeField]
    private float typeCounter;

    [SerializeField]
    private GameObject ice;

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private GameObject elec;

    [SerializeField]
    private float typeTimer;

    [SerializeField]
    public float strenght;

    [SerializeField]
    private Image typesCir;

    [SerializeField]
    private float[] angles;

    [SerializeField]
    private GameObject[] holders;

	// Use this for initialization
	void Start () {
		//currHP = maxHP;
        
        playerType = GameObject.Find("PlayerMelee");
        maxHP = playerType.GetComponent<PlayerType>().maxHealth;
        currHP = maxHP;
        typeCounter = 0;
        typeTimer = 7;
	}

	
	// Update is called once per frame
	void Update () {
        ManageType();
        HealthManager();
        if (typeCounter < 21)
            typeCounter += Time.deltaTime;
        else if (typeCounter >= 21)
            typeCounter = 0;
        timeBar.fillAmount = typeTimer / 7;
        typeTimer -= Time.deltaTime;
	}

    void    ManageType()
    {
        if (typeCounter >= 0 && typeCounter < 7)
        {
            
            fire.active = false;
            ice.active = false;
            elec.active = true;
            playerType = elec;
            holders[0].active = true;
            holders[1].active = false;
            holders[2].active = false;
            typesCir.rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(angles[0], typesCir.rectTransform.rotation.z, 0.01f));
            if (typeTimer <= 0)
            {
                ShiftSound();
                typeTimer = 7;
            }
        }
        else if (typeCounter >= 7f && typeCounter < 14)
        {
            
            holders[0].active = false;
            holders[1].active = true;
            holders[2].active = false;
            fire.active = false;
            ice.active = true;
            elec.active = false;
            playerType = ice;
            typesCir.rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(angles[1], typesCir.rectTransform.rotation.z, 0.01f));
            if (typeTimer <= 0)
            {
                ShiftSound();
                typeTimer = 7;
            }
        }
        else if (typeCounter >= 14 && typeCounter < 21)
        {
            
            holders[0].active = false;
            holders[1].active = false;
            holders[2].active = true;
            fire.active = true;
            ice.active = false;
            elec.active = false;
            playerType = fire;
            typesCir.rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(angles[2], typesCir.rectTransform.rotation.z, 0.01f));;
            if (typeTimer <= 0)
            {
                ShiftSound();
                typeTimer = 7;
            }
        }
    }

    [SerializeField]
    private GameObject shiftSound;

    void    ShiftSound()
    {
        GameObject temp = Instantiate(shiftSound, transform.position, transform.rotation);
        Destroy(temp, 1f);
    }

    void    HealthManager()
    {
        maxHP = playerType.GetComponent<PlayerType>().maxHealth;
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currHP / maxHP, lerpSpeed);
        if (currHP <= 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Destroy(this.gameObject);
            
        }
        if (currHP > maxHP)
            currHP = maxHP;
    }

	public	void	TakeDamage(float damage)
	{
		currHP -= damage;
	}

    public  void    Heal(float heal)
    {
        currHP += heal;
    }

}
