using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Attack : MonoBehaviour
{
    public Text attackNum;
    public GameObject input_E;

    [HideInInspector]
    public float knokTime;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public float animTime;
    [HideInInspector]
    public bool attack;
    ParticleSystem attackFX;
    float timer;

    bool startAttack;
    //[HideInInspector]
    public int attackCounter;


    void Start()
    {
        attackFX = transform.GetChild(0).GetComponent<ParticleSystem>();
        if (knokTime == 0)
        {
            knokTime = 1000000000f;
        }
        input_E.SetActive(false);

    }

    private void Update()
    {
        attackNum.text = attackCounter.ToString();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("GuardKeySlot") == true)
        {
            input_E.SetActive(true);

            canAttack = true;

            if (attack)
            {
                input_E.SetActive(false);
                if ( startAttack == false)
                {
                    timer = animTime;
                    startAttack = true;
                    canAttack = false;
                    Debug.Log("attack");
                }

                timer -= Time.deltaTime;
                if (startAttack == true)
                {
                    if (timer <= 0)
                    {

                        attackFX.Play();
                        attack = false;
                        startAttack = false;
                        other.transform.parent.GetComponent<PatrolAction>()._knoked = true;
                        other.transform.parent.GetComponent<PatrolAction>()._waiting = true;
                        other.transform.parent.GetComponent<PatrolAction>()._waitTimer = 0f;
                        other.transform.parent.GetComponent<PatrolAction>()._totalWaitTime = knokTime;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canAttack = false;
        input_E.SetActive(false);
    }
}