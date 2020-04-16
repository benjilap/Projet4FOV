using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float travelTime;
	public Transform playerTrans;
	private Vector3 posCam;
    Vector3 camVelocity;

	void Start(){
		posCam = new Vector3 (0,10,0);
        transform.position = playerTrans.position + posCam;
    }

	void Update(){
        //playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
        transform.position = Vector3.SmoothDamp(this.transform.position, playerTrans.position + posCam, ref camVelocity, travelTime);

    }
}
