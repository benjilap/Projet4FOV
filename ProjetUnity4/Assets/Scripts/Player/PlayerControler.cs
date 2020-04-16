using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

	private Vector3 playerMove;
	private Rigidbody rb;
	private Quaternion lastRot;
    private Transform Fow;
	public float speed;

    Attack myattack;

	void Start () {
		rb = GetComponent<Rigidbody> ();
        Fow = this.transform.GetChild(0);
        myattack= this.transform.GetChild(2).GetComponent<Attack>();
	}

	void Update () 
	{
        Fow.rotation = lastRot;

        if (myattack.canAttack == true && myattack.attackCounter >0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                myattack.attack = true;
            }
        }
        if (myattack.attack == false)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                playerMove = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                playerMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            }
        }
        if (playerMove != Vector3.zero) { 
		    lastRot = Quaternion.LookRotation (playerMove.normalized);
        }


        rb.velocity = playerMove.normalized * speed;

    }



}
