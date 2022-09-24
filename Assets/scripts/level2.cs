using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2 : MonoBehaviour
{
    public GameObject portal;
    public Canvas qC1, qC2, qC3;
    // public static int score = 0, lives = 5, solvedQ = 0, numQ = 3;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.solvedQ = 0;
        PlayerController.numQ = 3;
        PlayerController.lives = 2;
        qC1.GetComponent<Canvas> ().enabled = false;
        qC2.GetComponent<Canvas> ().enabled = false;
        qC3.GetComponent<Canvas> ().enabled = false;
        portal.SetActive(false);
    }

    void OnTriggerEnter(Collider player){
        if(player.CompareTag("Player")){
            qC1.GetComponent<Canvas> ().enabled = true;
            qC2.GetComponent<Canvas> ().enabled = true;
            qC3.GetComponent<Canvas> ().enabled = true;
        }

        if(PlayerController.solvedQ == PlayerController.numQ){
            Destroy(this);
        }
    }
}
