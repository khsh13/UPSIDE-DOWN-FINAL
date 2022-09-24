using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leve1 : MonoBehaviour
{
    public GameObject portal;
    public Canvas qC1, qC2, qC3, qC4, qC5;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.solvedQ = 0;
        PlayerController.numQ = 5;
        PlayerController.lives = 2;
        qC1.GetComponent<Canvas> ().enabled = false;
        qC2.GetComponent<Canvas> ().enabled = false;
        qC3.GetComponent<Canvas> ().enabled = false;
        qC4.GetComponent<Canvas> ().enabled = false;
        qC5.GetComponent<Canvas> ().enabled = false;
        portal.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnTriggerEnter(Collider player){
        if(player.CompareTag("Player")){
            qC1.GetComponent<Canvas> ().enabled = true;
            qC2.GetComponent<Canvas> ().enabled = true;
            qC3.GetComponent<Canvas> ().enabled = true;
            qC4.GetComponent<Canvas> ().enabled = true;
            qC5.GetComponent<Canvas> ().enabled = true;
        }
    }
}
