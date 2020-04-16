using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoadCine : MonoBehaviour {

    [SerializeField] float Timer = 30f;
    float compteur;
    [SerializeField] Text Continue;
    [SerializeField] string level;

    private void Start()
    {
        compteur = Time.time;
    }


    private void Update()
    {
        if (Time.time - compteur > Timer)
        {
            Continue.enabled = true;
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene(level);
            }
        }


    }



}
