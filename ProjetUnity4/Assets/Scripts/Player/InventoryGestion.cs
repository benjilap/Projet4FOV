using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGestion : MonoBehaviour
{
    public GameObject input_A;
    string input_AText;

    public List<GameObject> Keys = new List<GameObject>();

    public List<Image> Slots = new List<Image>();

    Attack myattack;
    Sprite defaultSprite;
    public int intSlot;

    void Start()
    {
        defaultSprite = Slots[0].GetComponent<Image>().sprite;
        Keys.Add(null);
        Keys.Add(null);
        Keys.Add(null);
        intSlot = 3;
        myattack = this.GetComponent<Attack>();
        input_A.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        InteractPos();
        UpdateSlot();
    }

    void InteractPos()
    {
        if (this.transform.parent.GetComponent<Rigidbody>().velocity.x == 0)
        {
            if (this.transform.parent.GetComponent<Rigidbody>().velocity.z < 0)
            {
                transform.position = this.transform.parent.position + new Vector3(0, 0, -0.25f);
            }
            if (this.transform.parent.GetComponent<Rigidbody>().velocity.z > 0)
            {
                transform.position = this.transform.parent.position + new Vector3(0, 0, 0.25f);
            }
        }
        else
        if (this.transform.parent.GetComponent<Rigidbody>().velocity.x != 0)
        {
            if (this.transform.parent.GetComponent<Rigidbody>().velocity.x < 0)
            {
                transform.position = this.transform.parent.position + new Vector3(-0.25f, 0, 0);
            }
            if (this.transform.parent.GetComponent<Rigidbody>().velocity.x > 0)
            {
                transform.position = this.transform.parent.position + new Vector3(0.25f, 0, 0);
            }
        }
    }

    public void SlotSprite()
    {
        if (intSlot < 2)
        {
            for (int i = 0; i < Keys.Count - 1; i++)
            {
                if (Keys[i + 1] != null)
                {
                    if (Keys[i] == null)
                    {
                        Keys[i] = Keys[i + 1];
                        Keys[i + 1] = null;

                    }
                }
            }
        }
    }

    void UpdateSlot()
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            if (Keys[i] != null)
            {
                Slots[i].sprite = Keys[i].GetComponent<ObjectSpriteScript>().slotSprite;
            }
            else
            {
                Slots[i].sprite = defaultSprite;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("KeySlot") == true || other.CompareTag("GuardKeySlot") == true)
        {
            input_AText = "Intéragir";
            input_A.transform.GetChild(1).GetComponent<Text>().text = input_AText;

            if (other.GetComponent<KeyStock>().Key != null)
            {
                input_A.SetActive(true);
                if (Input.GetKeyDown(KeyCode.A) == true)
                {
                    if (other.GetComponent<KeyStock>().Key.name == "Stick")
                    {
                        input_A.SetActive(false);
                        myattack.attackCounter += 1;
                        other.GetComponent<KeyStock>().Key = null;
                    }
                    else
                    {
                        input_A.SetActive(false);

                        Keys[Keys.Count - intSlot] = other.GetComponent<KeyStock>().Key;
                        intSlot -= 1;

                        other.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        other.GetComponent<KeyStock>().Key = null;
                        SlotSprite();
                    }
                }


            }
        }


        if (other.CompareTag("PC") == true)
        {
            input_AText = "Endommager PC";
            Debug.Log("touchPC");
            input_A.transform.GetChild(1).GetComponent<Text>().text = input_AText;
            if (other.GetComponent<CameraMonitor>().cracked == false)
            {
                input_A.SetActive(true);
                if (Input.GetKeyDown(KeyCode.A) == true)
                {
                    input_A.SetActive(false);
                    Debug.Log("crackpc");
                    other.GetComponent<CameraMonitor>().cracked = true;
                    other.GetComponent<CameraMonitor>().DesactivateCam();
                }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        input_A.SetActive(false);
    }
}
