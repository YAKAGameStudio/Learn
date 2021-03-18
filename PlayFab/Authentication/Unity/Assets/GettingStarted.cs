﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class GettingStarted : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var LoginRequest = new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(LoginRequest, OnLoginSuccess, OnLoginError);

	}

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulation, you are authenticated");
        Debug.LogFormat("Login Success: {0}", result.PlayFabId);
        Debug.LogFormat("Session: {0}", result.SessionTicket);
    }

    private void OnLoginError(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

}
