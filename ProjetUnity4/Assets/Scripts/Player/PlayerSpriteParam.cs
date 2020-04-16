using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteParam : MonoBehaviour
{

    Animator playerAtor;
    Rigidbody playerRb;
    int animInt;

    Attack myattack;

    void Start()
    {
        playerAtor = GetComponent<Animator>();
        playerRb = this.transform.parent.GetComponent<Rigidbody>();
        playerAtor.SetInteger("Idle", 1);
        myattack = this.transform.parent.GetChild(2).GetComponent<Attack>();
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        if (myattack.attack == false)
        {
            playerAtor.SetBool("Attack", false);
            if (playerRb.velocity.x <= -0.1|| playerRb.velocity.x >= 0.1|| playerRb.velocity.z >= 0.1 || playerRb.velocity.z <= -0.1)
            {
                playerAtor.SetBool("Walk", true);
            }
            else
            {
                playerAtor.SetBool("Walk", false);
            }

            AnimDir();
            playerAtor.SetInteger("Idle", animInt);
        }
        else
        {
            playerAtor.SetBool("Attack", true);
        }
    }

    void AnimDir()
    {
        if (playerRb.velocity.x == 0)
        {
            if (playerRb.velocity.z < -0.1)
            {
                animInt = 1;
            }
            if (playerRb.velocity.z > 0.1)
            {
                animInt = 3;
            }
        }else
        if (playerRb.velocity.x != 0)
        {
            if (playerRb.velocity.x < -0.1)
            {
                animInt = 4;
            }
            if (playerRb.velocity.x > 0.1)
            {
                animInt = 2;
            }
        }
        
    }
}
