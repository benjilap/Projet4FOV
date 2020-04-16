using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpriteParam : MonoBehaviour
{

    Animator camAtor;

    float angleRange= 22.5f;
    int animInt;

    Vector3 dirLook;
    float anglePos;

    void Start()
    {
        camAtor = GetComponent<Animator>();
        anglePos = this.transform.parent.GetChild(0).GetComponent<SecurityCam>().angleInit;
        Debug.Log(anglePos);
        AnimDir(anglePos);
        camAtor.SetInteger("Pos", animInt);
    }


    void Update()
    {
        dirLook = this.transform.parent.GetChild(0).transform.eulerAngles;

        AnimDir(dirLook.y);
        camAtor.SetInteger("Pos", animInt);

    }

    void AnimDir(float angle)
    {
        if (angle > 180 + angleRange)
        {
            if (angle < 270 - angleRange)
            {
                animInt = 8;

            }
        }
        if (angle <= 270 + angleRange)
        {
            if (angle >= 270 - angleRange)
            {
                animInt = 7;

            }
        }

        if (angle > 270 + angleRange)
        {
            if (angle < 360 - angleRange)
            {
                animInt = 6;

            }
        }
        if (angle <= 0 + angleRange|| angle >= 360 - angleRange)
        {
            animInt = 5;

        }

        if (angle > 0 + angleRange)
        {
            if (angle < 90 - angleRange)
            {
                animInt = 4;

            }
        }

        if (angle <= 90 + angleRange)
        {
            if (angle >= 90 - angleRange)
            {
                animInt = 3;

            }
        }

        if (angle > 90 + angleRange)
        {
            if (angle < 180 - angleRange)
            {
                animInt = 2;

            }
        }

        if (angle <= 180 + angleRange)
        {
            if (angle >= 180 - angleRange)
            {
                animInt = 1;

            }
        }


    }
}
