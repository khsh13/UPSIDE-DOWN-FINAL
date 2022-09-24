using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playfab_controller : MonoBehaviour {

    private string userEmail;
    private string userPassword;
    private string username;
    public GameObject loginPanel;
    public static playfab_controller PFC;
    // public Text messageText;

    public void Awake()
    {
        answerCheck.OnUpdate += SendLeaderboard;
        // PlayerController.lives -= SendLeaderboard;
    }

    private void OnEnable() {
        if (playfab_controller.PFC == null) {
            playfab_controller.PFC = this;
        }
        else {
            if(playfab_controller.PFC != this) {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) {
            PlayFabSettings.TitleId = "61414"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        if (PlayerPrefs.HasKey("EMAIL")) {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest{Email = userEmail, Password = userPassword};
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        

    }

    #region Login
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        GetStats();
    }

    private void OnRegisterSuccess (RegisterPlayFabUserResult result) {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        GetStats();
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest {Email = userEmail, Password = userPassword, Username = username};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterFailure(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetUserEmail(string emailIn) {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn) {
        userPassword = passwordIn;
    }

    public void GetUsername (string usernameIn) {
        username = usernameIn;
    }

    public void OnClickLogin() {
        var request = new LoginWithEmailAddressRequest{Email = userEmail, Password = userPassword};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    #endregion
    
    public int playerLevel;
    public int gameLevel;
    public int playerDamage;
    public int playerHealth;
    public int playerHighScore;

    #region PlayerStats

    public void SetStats() {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerLevel", Value = playerLevel},
        new StatisticUpdate { StatisticName = "GameLevel", Value = gameLevel},
        new StatisticUpdate { StatisticName = "PlayerDamage", Value = playerDamage},
        new StatisticUpdate { StatisticName = "PlayerHighScore", Value = playerHighScore},
        new StatisticUpdate { StatisticName = "PlayerHealth", Value = playerHealth},
            }
        },

    result => {Debug.Log("User statistics updated"); },
    error => {Debug.LogError(error.GenerateErrorReport()); });
    }
    
    void GetStats() {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStats(GetPlayerStatisticsResult result) {
        Debug.Log("Received the following Statistics:");
        foreach(var eachStat in result.Statistics) {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch(eachStat.StatisticName) {
                case "PlayerLevel":
                    playerLevel = eachStat.Value;
                    break;
                case "GameLevel":
                    gameLevel = eachStat.Value;
                    break;
                case "PlayerDamage":
                    playerDamage = eachStat.Value;
                    break;
                case "PlayerHighScore":
                    playerHighScore = eachStat.Value;
                    break;
                case "PlayerHealth":
                    playerHealth = eachStat.Value;
                    break;
            }
        }
    }

    public void SendLeaderboard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Weapons Collected",
                    Value = PlayerController.Weapons
                },
                new StatisticUpdate {
                    StatisticName = "Score",
                    Value = PlayerController.score
                }
            }
        }; 
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnError(PlayFabError error)
    {
        // messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successful leaderboard sent");
    }

    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "Weapons Collected",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result) {
        foreach (var item in result.Leaderboard) {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

   // public void StartCloudUpdatePlayerStats() {
       // PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest() {
         //   FunctionName = "UpdatePlayerStats",
         //   FunctionParameter = new {Level = playerLevel, highScore = playerHighScore},
         //   GeneratePlayStreamEvent = true,
       // }, OnCloudUpdateStats, OnErrorShared);
   // }

    //private static void OnCloudUpdateStats(ExecuteCloudScriptResult result) {
       // Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
//JsonObject jsonResult = (JsonObject)result.FunctionResult;
       //object messageValue;
       // jsonResult.TryGetValue("messageValue", out messageValue);
       // Debug.Log((string)messageValue);
   // }

   // private static void OnErrorShared(PlayFabError error) {
      //  Debug.Log(error.GenerateErrorReport());
   // }

    #endregion PlayerStats

    
    
}