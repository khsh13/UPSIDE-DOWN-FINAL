using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3 : MonoBehaviour
{
    public GameObject portal;
    public Canvas qC1;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.solvedQ = 0;
        PlayerController.numQ = 1;
        PlayerController.lives = 2;
        qC1.GetComponent<Canvas> ().enabled = false;
    
        portal.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnTriggerEnter(Collider player){
        if(player.CompareTag("Player")){
            qC1.GetComponent<Canvas> ().enabled = true;
        }
    }
}
