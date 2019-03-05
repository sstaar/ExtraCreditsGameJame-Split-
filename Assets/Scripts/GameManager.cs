using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static bool gMExist = false;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		DontDestroyOnLoad(this.gameObject);
		if (gMExist)
			Destroy(this.gameObject);
		else
			gMExist = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerStats>().currHP <= 0)
		{
			gMExist = false;
			
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			Destroy(this.gameObject);
		}
	}
}
