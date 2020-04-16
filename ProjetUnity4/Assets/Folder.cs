using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Folder : MonoBehaviour {


    private bool isReading = false;
    [SerializeField] Image archive;

	// Update is called once per frame
	void LateUpdate ()
    {
		if (isReading == true && archive.enabled == true && Input.GetKeyDown(KeyCode.E))
        {
            isReading = false;
            archive.enabled = false;
        }
	} 

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.A) && isReading == false)
        {
            isReading = true;
            archive.enabled = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {

            isReading = false;
            archive.enabled = false;

    }


}
