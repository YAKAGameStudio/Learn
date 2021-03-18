# LoginWithCustomID in Unity
Get started with the PlayFab Client library for Unity on Authentication mechanism discovery. 
We will first perform some test with a basic structure, then move to a more complete approach for mobile devices and perform the same in an MVC ASP.Net Core Website.

This quickstart helps you make your PlayFab API call in the using the Client library for C# in Unity. Before continuing, make sure you have a PlayFab account and are familiar with the PlayFab Game Manager. You can also review how to manage authentication in C# Console

If you need an overview about solutions, check Player Authentication mechanisms in PlayFab

> You can found the full code on YAKAGameStudio/Learn (github.com)

## Requirements
- A PlayFab developer account.
- An installation of Visual Studio.
- An installation of Unity

## Download & Install PlayFab SDK
Follow How to install PlayFab into Unity setup the prerequisites

##Set up your Authentication Call
You want to go through the final step ? check this file on GitHub

1. In Create a new Game Object, and rename it to PlayFab.
2. In the Assets window, Right-click and select Create > C# Script. 
3. Name the script GetStarted.
4. Add it to the Game Object as shown on the picture below.
5. Double-click the file to open it in a code-editor. Depending on your settings/installed-programs, this is likely Visual Studio or MonoDevelop.
6. In your code editor, replace the contents of PlayFabLogin.cs with the code shown below and save the file.

> For this demo, all code lines will stay into the GetStarted.cs file. We will see later how to structure the authentication class

## Initiate request header
When initiate your request, you should define how CustomID look like and behave.
```
var LoginRequest = new LoginWithCustomIDRequest
{
   // Custom unique identifier for the user, use anything you want.
   CustomId = "ConsoleAuthentication",
   // Automatically create a PlayFab account if one is not currently linked to this ID.
   CreateAccount = true,
   // Request additional Information
   InfoRequestParameters = LoginRequestParams
};
```
You can also request additional information on login to initialize your game or your player profile.

For more information see : getplayercombinedinforequestparams
```
var LoginRequestParams = new GetPlayerCombinedInfoRequestParams
{
  GetPlayerProfile = true
  GetUserVirtualCurrency = true
};
```
## Call PlayFab API
You are ready to send your authentication request and catch the answer. In the Main Function, we add the following
``
PlayFabClientAPI.LoginWithCustomIDAsync(LoginRequest)
  .ContinueWith(OnLoginCompleted)
  .Wait();
``
## Manage PlayFab answer
We create a new function (OnLoginCompleted) that will print confirmation of the API call using messages written to the console output.
