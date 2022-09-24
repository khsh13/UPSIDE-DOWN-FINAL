using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class playfab_login : MonoBehaviour {

    [SerializeField] GameObject signuptab, logintab, startPanel, HUD;
    public Text username, userEmail, userPassword, userEmailLogin, userPasswordLogin, errorSignup, errorLogin;
    string encryptedPassword; 

    public void SwitchtoSignUpTab() {
        signuptab.SetActive(true);
        logintab.SetActive(false);
        errorSignup.text = "";
        errorLogin.text = "";
    }

    public void SwitchtoLoginTab() {
        signuptab.SetActive(false);
        logintab.SetActive(true);
        errorSignup.text = "";
        errorLogin.text = "";
    }

    string Encrypt(string pass) {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach(byte b in bs) {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }

    public void SignUp() {
        var registerRequest = new RegisterPlayFabUserRequest{Email = userEmail.text, Username = username.text, Password = Encrypt(userPassword.text)};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterError);
    }

    public void RegisterSuccess(RegisterPlayFabUserResult result) {
        errorSignup.text = "";
        errorLogin.text = "";
        StartGame();
    }

    public void RegisterError(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }

    public void LogIn() {
        var request = new LoginWithEmailAddressRequest {Email = userEmailLogin.text, Password = Encrypt(userPasswordLogin.text)};
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LogInError);
    }

    public void LoginSuccess(LoginResult result) {
        errorSignup.text = "";
        errorLogin.text = "";
        StartGame();
    }

    public void LogInError(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }

    void StartGame() {
        startPanel.SetActive(false);
        HUD.SetActive(true);
    }

}