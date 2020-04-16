using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyStock : MonoBehaviour {

    public GameObject Key;
    public ParticleSystem KeyRed;
    public ParticleSystem KeyGreen;
    public ParticleSystem KeyYellow;
    public ParticleSystem StickEffect;
    GameObject pickEffect;

    void Start()
    {

        if (Key.name == "Stick")
        {
            pickEffect = Instantiate(StickEffect.gameObject, transform.position, Quaternion.Euler(90, 0, 0));
            pickEffect.transform.parent = transform;
            pickEffect.transform.position = transform.position;
            if (this.GetComponent<SpriteRenderer>() != null)
            {
                this.GetComponent<SpriteRenderer>().sprite = Key.GetComponent<ObjectSpriteScript>().slotSprite;
            }
        }

        if (Key.name == "RedKey")
        {
            pickEffect = Instantiate(KeyRed.gameObject, transform.position, Quaternion.Euler(90, 0, 0));
            pickEffect.transform.parent = transform;
            pickEffect.transform.position = transform.position;
            if (this.GetComponent<SpriteRenderer>() != null)
            {
                this.GetComponent<SpriteRenderer>().sprite = Key.GetComponent<ObjectSpriteScript>().slotSprite;
            }

        }

        if (Key.name == "GreenKey")
        {
            pickEffect = Instantiate(KeyGreen.gameObject, transform.position, Quaternion.Euler(90, 0, 0));
            pickEffect.transform.parent = transform;
            pickEffect.transform.position = transform.position;
            if (this.GetComponent<SpriteRenderer>() != null)
            {
                this.GetComponent<SpriteRenderer>().sprite = Key.GetComponent<ObjectSpriteScript>().slotSprite;
            }
        }

        if (Key.name == "YellowKey")
        {
            pickEffect = Instantiate(KeyYellow.gameObject, transform.position, Quaternion.Euler(90, 0, 0));
            pickEffect.transform.parent = transform;
            pickEffect.transform.position = transform.position;
            if (this.GetComponent<SpriteRenderer>() != null)
            {
                this.GetComponent<SpriteRenderer>().sprite = Key.GetComponent<ObjectSpriteScript>().slotSprite;
            }
        }
    }


    private void Update()
    {
        if (transform.parent != null)
        {
            if (transform.parent.tag == "Guard")
            {
                transform.rotation = Quaternion.Euler(0, -transform.parent.rotation.y, 0);
            }
        }else
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        if (this.GetComponent<SpriteRenderer>() != null)
        {
            if (Key == null)
            {
                this.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

}
