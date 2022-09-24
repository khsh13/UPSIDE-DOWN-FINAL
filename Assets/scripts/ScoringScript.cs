using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour {

    public GameObject weaponText;
    // public static int Weapons, totalW;
    public AudioSource collectSound;

    void OnTriggerEnter(Collider other) {
        collectSound.Play();
        PlayerController.Weapons = PlayerController.Weapons + 1;
        weaponText.GetComponent<Text>().text = "WEAPONS COLLECTED: " + PlayerController.Weapons;
        Destroy(gameObject);
    }
}