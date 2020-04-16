using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardSpriteParam : MonoBehaviour {


    Animator guardAtor;
    NavMeshAgent guardRb;
    int animInt;
    ParticleSystem walk_FX;
    bool walk;
    Vector3 lastVel;

    void Start()
    {
        guardAtor = GetComponent<Animator>();
        guardRb = this.transform.parent.GetComponent<NavMeshAgent>();
        walk_FX = this.transform.parent.GetChild(4).GetComponent<ParticleSystem>();

        guardAtor.SetInteger("Idle", 1);
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        

        if (guardRb.velocity != Vector3.zero)
        {
            lastVel = guardRb.velocity.normalized;
            guardAtor.SetBool("Walk", true);
            if (walk == false)
            {
                walk = true;
                walk_FX.Play();
            }
        }
        else
        {
            guardAtor.SetBool("Walk", false);
            if (walk == true)
            {
                walk_FX.Stop();
            }
        }

        AnimDir();
        guardAtor.SetInteger("Idle", animInt);

    }

    void AnimDir()
    {
        if (lastVel.x < -0.5f)
        {
            if (lastVel.z < 0.5f)
            {
                if (lastVel.z > -0.5f)
                {
                    animInt = 4;
                }
            }
        }
        else

        if (lastVel.x > 0.5f)
        {
            if (lastVel.z < 0.5f)
            {
                if (lastVel.z > -0.5f)
                {
                    animInt = 2;
                }
            }
        }
        else

        if (lastVel.z < -0.5f)
        {
            if (lastVel.x < 0.5f)
            {
                if (lastVel.x > -0.5f)
                {
                    animInt = 1;
                }
            }
        }
        else
        if (lastVel.z > 0.5f)
        {
            if (lastVel.x < 0.5f)
            {
                if (lastVel.x > -0.5f)
                {
                    animInt = 3;
                }
            }
        }

    }
}
