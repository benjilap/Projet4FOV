using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMonitor : MonoBehaviour {

    public List<GameObject> myCamerasList;
    [HideInInspector]
    public bool cracked;
    public string camColor;
    public Sprite redScreen;
    public Sprite yellowScreen;
    public Sprite greenScreen;

    private void Start()
    {
        if (camColor == "R")
        {
            this.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = redScreen;
        }
        if (camColor == "J")
        {
            this.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = yellowScreen;
        }
        if (camColor == "G")
        {
            this.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = greenScreen;
        }
    }

    public void DesactivateCam()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Play(true);
        this.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
        for (int i = 0; i < myCamerasList.Count; i++)
        {
            myCamerasList[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
	
}
