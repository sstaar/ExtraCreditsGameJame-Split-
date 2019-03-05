using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	[SerializeField]
	private	Image[] imgs;


	// Use this for initialization
	void Start () {
		StartCoroutine(Story());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator		Story()
	{
		Destroy(imgs[0], 3f);
		Destroy(imgs[1], 7f);
		Destroy(imgs[2], 9f);
		Destroy(imgs[3], 13f);
		Destroy(imgs[4], 16);
		yield return new WaitForSeconds(26f);
		SceneManager.LoadScene("Stage1");
		//Destroy(imgs[4], 16);
	}
}
