using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Import SDK
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;

public class FBAuthentication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            Debug.Log("Facebook: Initialize SDK");
            FB.ActivateApp();
            
            // Facebook Login Message
            var FacebookLoginRequest = new List<string>() 
            { 
                "public_profile", 
                "email" 
            };

            // Facebook Login
            FB.LogInWithReadPermissions(FacebookLoginRequest, OnFacebookLogin);

        }
        else
        {
            Debug.LogWarning("Facebook: Failed to Initialize SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void OnFacebookLogin(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook SDK : User requested login");

            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            // Setup PlayFab Login Message 
            var PlayFabLoginRequest = new LoginWithFacebookRequest ()
            {
                // Your Title setup in PlayFab SDK
                TitleId = PlayFabSettings.TitleId,
                // Automatically create a PlayFab account if one is not currently linked to this ID.
                CreateAccount = true,
                // Provide current Facebook AccessToken
                AccessToken = AccessToken.CurrentAccessToken.TokenString
            };

            // Call PlayFab API 
            PlayFabClientAPI.LoginWithFacebook(PlayFabLoginRequest, OnPlayfabLogin, OnPlayfabError);
        }
        else
        {
            Debug.Log("Facebook SDK : User cancelled login");
        }
    }

    private void OnPlayfabLogin(PlayFab.ClientModels.LoginResult result)
    {
        Debug.Log("PlayFab: Congratulation, you are authenticated");
        Debug.LogFormat("PlayFab: Login Success: {0}", result.PlayFabId);
        Debug.LogFormat("PlayFab: Session: {0}", result.SessionTicket);
    }

    private void OnPlayfabError(PlayFabError error)
    {
        Debug.LogWarning("PlayFab: Something went wrong with your first API call.  :(");
        Debug.LogError("PlayFab: Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

}
