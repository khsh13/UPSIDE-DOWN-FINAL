using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
 //public GameObject[] weapons;
 public GameObject weaponText;
 public delegate void ScoreUpdate(int value);
    public static event ScoreUpdate OnUpdate;
 //public GameObject[] questions;
 //private bool anwered = false;
 // private bool popup = false;
 
 //public GameObject demogorgon;
 //public static GameManager Instance;
 //public GameState state;
 //public GameObject weaponText;
//  public static int Weapons;
 public AudioSource collectSound;
 //public static event Action<GameState> OnGameStateChanged;
 //void Awake() {
 // Instance = this;
 //}
 void Start() {
 //UpdateGameState(GameState.lives);
//  isActive = false;
 }

#region gameoutputs
//  Scene currentScene = SceneManager.GetActiveScene();
//  string sceneName = currentScene.name;
 public GameObject portal;
 public void GameOverWon() {
 if(portal.activeSelf) {
 SceneManager.LoadScene(7);
 GameStatsSent();
 }
 }
 public void GameStatsSent() {
//  playfab_controller.SendLeaderboard(PlayerController.score);
OnUpdate(PlayerController.score);
 }
 public void GameOverLost() {
 SceneManager.LoadScene(8);
 }
#endregion
}