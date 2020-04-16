using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour {

    [SerializeField] Text archive;
    [SerializeField] int Time = 5;
    bool hasPlayed = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && hasPlayed == false)
        {
            StartCoroutine(raconte());
        }
    }

    IEnumerator raconte()
    {
        archive.enabled = true;
        hasPlayed = true;
        yield return new WaitForSeconds(Time);
        archive.enabled = false;
        yield return null;
    }

}
