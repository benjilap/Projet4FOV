using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public bool sideDoor;
    public string keyColor;

    string doorKey;
    private Animator doorAtor1;
    private Animator doorAtor2;
    GameObject side;
    GameObject face;

    bool opened;

    void Start () {
        side = transform.GetChild(1).gameObject;
        face = transform.GetChild(0).gameObject;

        if (sideDoor) {
            face.SetActive(false);
            doorAtor1 = side.transform.GetChild(0).GetComponent<Animator>();
            doorAtor2 = side.transform.GetChild(1).GetComponent<Animator>();
        }
        else
        {
            side.gameObject.SetActive(false);
            doorAtor1 = face.GetComponent<Animator>();
        }

        if (keyColor == "N")
        {
            doorKey = null;

        }
        if (keyColor == "Y")
        {
            doorKey = "YellowKey";

        }
        if (keyColor == "R")
        {
            doorKey = "RedKey";

        }

        if (keyColor == "G")
        {
            doorKey = "GreenKey";

        }

    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.z < transform.position.z-0.1f)
        {
            face.GetComponent<SpriteRenderer>().sortingOrder = -2;
        }else { face.GetComponent<SpriteRenderer>().sortingOrder = 0; }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            GameObject player = other.gameObject;

            if (doorKey == null)
            {
                Open();
            }
            else
            {

                if (other.gameObject.name == "Player")
                {
                    InventoryGestion playerInventory = player.transform.GetChild(2).GetComponent<InventoryGestion>();

                    for (int i = 0; i < playerInventory.Keys.Count; i++)
                    {

                        if (playerInventory.Keys[i] != null)
                        {
                            if (playerInventory.Keys[i].name == doorKey)
                            {
                                Open();
                                playerInventory.Keys[i] = null;
                                playerInventory.SlotSprite();

                                playerInventory.intSlot += 1;


                            }
                        }
                    }
                }
            }
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (opened == true)
        {
            if (other.gameObject.name == "Player")
            {
                Closed();
            }
        }
    }
    void Open() {
        opened = true;
        if (sideDoor)
        {
            side.GetComponent<Collider>().enabled = false;
            doorAtor1.SetBool("Ouvert", true);
            doorAtor2.SetBool("Ouvert", true);

        }
        else
        {
            face.GetComponent<Collider>().enabled = false;
            doorAtor1.SetBool("Ouvert", true);
        }

    }
    void Closed()
    {
        opened = false;
        if (sideDoor)
        {
            side.GetComponent<Collider>().enabled = true;
            doorAtor1.SetBool("Ouvert", false);
            doorAtor2.SetBool("Ouvert", false);

        }
        else
        {
            face.GetComponent<Collider>().enabled = true;
            doorAtor1.SetBool("Ouvert", false);
        }
    }
}
