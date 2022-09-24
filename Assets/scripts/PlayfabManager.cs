// using System.Collections.Generic;
// using PlayFab;
// using PlayFab.ClientModels;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class PlayFabManager : MonoBehaviour
// {
//     public Text messageText;

//     public InputField emailInput;

//     public InputField passwordInput;

//     //public bool isDone = false;
//     public int ans;

//     public void Awake()
//     {
//         // GoldPickup.OnUpdate += SendLeaderboard;
//         // HealthManager.OnUpdate += SendLeaderboard;
//         // CanvasInput.OnUpdate += SendLeaderboard;
//         // QUIT.OnUpdate += SendLeaderboard;
//         // GameOver.OnUpdate += SendLeaderboard;
//         // Success.OnUpdate += SendLeaderboard;
//     }

//     public void LoginButton()
//     {
//         var request =
//             new LoginWithEmailAddressRequest {
//                 Email = emailInput.text,
//                 Password = passwordInput.text
//             };
//         PlayFabClientAPI.LoginWithEmailAddress (
//             request,
//             OnLoginSuccess,
//             OnError
//         );
//     }

//     void OnLoginSuccess(LoginResult result)
//     {        
//         GetStatistics();
//     }

//     void GetStatistics()
//     {
//         PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(),OnGetStatistics,error => Debug.LogError(error.GenerateErrorReport()));
//     }

//     void OnGetStatistics(GetPlayerStatisticsResult result)
//     {
//         Debug.Log("Received the following Statistics:");
//         // foreach (var eachStat in result.Statistics){
//         //     Debug.Log("Statistic (" +eachStat.StatisticName +"): " +eachStat.Value);
//         //     ans = eachStat.Value;
//         //     Debug.Log(ans);
//         // }
//         if(result.Statistics[0].Value != 0){
//             messageText.text = "You've already played";
//         }
//         else
//         {
//             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//             messageText.text = "Logged In";
//         }
//     }

//     public void RegisterButton()
//     {
//         var request =
//             new RegisterPlayFabUserRequest {
//                 Email = emailInput.text,
//                 Password = passwordInput.text,
//                 RequireBothUsernameAndEmail = false
//             };
//         PlayFabClientAPI.RegisterPlayFabUser (
//             request,
//             OnRegisterSuccess,
//             OnError
//         );
//     }

//     void OnRegisterSuccess(RegisterPlayFabUserResult result)
//     {
//         messageText.text = "Registered and logged in!";
//         SendLeaderboard(0);
//         //isDone = true;
//     }

//     void OnError(PlayFabError error)
//     {
//         messageText.text = error.ErrorMessage;
//     }

//     public void SendLeaderboard(int score)
//     {
//         Debug.Log (score);
//         var request =
//             new UpdatePlayerStatisticsRequest {
//                 Statistics =
//                     new List<StatisticUpdate> {
//                         new StatisticUpdate {
//                             StatisticName = "Score",
//                             Value = score
//                         }
//                     }
//             };
//         PlayFabClientAPI.UpdatePlayerStatistics (
//             request,
//             OnLeaderboardUpdate,
//             OnError
//         );
//     }

//     void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
//     {
//         Debug.Log (result);
//     }
// }