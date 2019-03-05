using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


    [SerializeField]
    float shakeAmount;
	// Use this for initialization
	/*void Awake()
	{
        if (mainCam == null)
            mainCam = Camera.main;
    }*/

    void    Start()
    {
        
    }

	public void Shake (float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    public void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = this.transform.position;

            //float shakeAmtX = Random.value * shakeAmount * 2 - shakeAmount;
            //float shakeAmtY = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeAmtX = Random.Range(-1f, 1f) * shakeAmount;
            float shakeAmtY = Random.Range(-1f, 1f) * shakeAmount;
            camPos.x += shakeAmtX;
            camPos.x += shakeAmtY;

            this.transform.position = camPos;
        }
        
    }
    public void StopShake()
    {
        CancelInvoke("BeginShake");
        this.transform.localPosition = Vector3.zero;
    }
}
