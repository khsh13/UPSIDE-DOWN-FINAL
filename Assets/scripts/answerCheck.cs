using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class answerCheck : MonoBehaviour
{
    public Button submit;
    public InputField answerField;
    public string answer;
    public GameObject scoreText, livesText, portal;
    public GameObject demogorgon;
    public delegate void ScoreUpdate(int value);
    public static event ScoreUpdate OnUpdate;

    // Start is called before the first frame update
    void Start()
    {
        submit.onClick.AddListener(GetInputOnClickHandler);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void GetInputOnClickHandler(){
        if(answerField.text == answer){
            PlayerController.solvedQ += 1;
            PlayerController.score += 5;
            scoreText.GetComponent<Text>().text = "SCORE: " + PlayerController.score;
            Debug.Log(PlayerController.score);
            this.GetComponent<Canvas> ().enabled = false;
            if(PlayerController.solvedQ == PlayerController.numQ){
                portal.SetActive(true);
                Destroy(demogorgon);
            }
        }
        else{
            PlayerController.lives--;
            if(PlayerController.lives <= 0){
                SceneManager.LoadScene(8);
            }
            else{
                livesText.GetComponent<Text>().text = "LIVES LEFT: " + PlayerController.lives;
            }
        }
        OnUpdate(PlayerController.score);
        OnUpdate(PlayerController.lives);
    }
}
