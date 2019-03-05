using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCurser : MonoBehaviour {


	[SerializeField]
	private float lerpVal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//float	x = Mathf.Lerp(this.transform.position.x, Input.mousePosition.x, lerpVal);
		//float	y = Mathf.Lerp(this.transform.position.y, Input.mousePosition.y, lerpVal);

		this.transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
		Cursor.visible = false;
	}
}
