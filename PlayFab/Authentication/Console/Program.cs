using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// Demonstrate how to authenticate user in PlayFab
/// Note: This is the first level based on LoginWithCustomID
/// See - https://yaka.studio/authentication/LoginWithCustomID-console
/// </summary>
namespace ConsoleAuthentication
{
    class Program
    {
        /// <summary>
        /// Main program used on startup
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the LoginWithCustomID Demo \n");

            // Provide your titleId from PlayFab Game Manager
            PlayFabSettings.staticSettings.TitleId = "YourTitleId";


            // Retrieve additional information
            // See - https://docs.microsoft.com/en-us/rest/api/playfab/client/authentication/loginwithcustomid?view=playfab-rest#getplayercombinedinforequestparams
            var LoginRequestParams = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
                GetUserVirtualCurrency = true

            };

            var CustomTags = new Dictionary<string, string>()
            {
                {"Build", "Dev"},
                {"Source", "Console"}
            };

            // Initiate Login Request
            var LoginRequest = new LoginWithCustomIDRequest
            {
                // Custom unique identifier for the user, use anything you want
                CustomId = "ConsoleAuthentication",
                
                // Automatically create a PlayFab account if one is not currently linked to this ID.
                CreateAccount = true,

                // The optional custom tags associated with the request (e.g. build number, external trace identifiers, etc.).
                CustomTags = CustomTags,

                // Request additional Information
                InfoRequestParameters = LoginRequestParams
            };

            // Call PlayFab API
            PlayFabClientAPI.LoginWithCustomIDAsync(LoginRequest)
                .ContinueWith(OnLoginCompleted)
                .Wait();

            // Wait for the user to respond before closing.
            Console.Write("Press any key to close the console app...");
            Console.ReadKey();
        }


        /// <summary>
        /// A simple example function that display results from the PlayFab Login
        /// </summary>
        /// <param name="loginTask">The requested login call</param>
        /// <returns>Only console line</returns>
        private static void OnLoginCompleted(Task<PlayFabResult<LoginResult>> loginTask)
        {
            var apiError = loginTask.Result.Error;
            var apiResult = loginTask.Result.Result;

            var PlayerProfile = apiResult.InfoResultPayload.PlayerProfile;
            var PlayerCurrencies = apiResult.InfoResultPayload.UserVirtualCurrency;

            if (apiError != null)
            {
                // When Error Occur
                Console.WriteLine("Something went wrong with your API call :( \nHere's some debug information:");
                
                // Print issue wthe the PlayFabUtil class
                Console.WriteLine(PlayFabUtil.GenerateErrorReport(apiError));
                Console.WriteLine(apiError.GenerateErrorReport());
            }
            else
            {
                // Display Player information
                Console.WriteLine("Congratulations, you made a successful API call! \n");
                Console.WriteLine("Player information:");
                
                // Player Session
                Console.WriteLine("PlayFabId:           " + apiResult.PlayFabId);
                Console.WriteLine("SessionTicket:       " + apiResult.SessionTicket);
                // Player Activity
                Console.WriteLine("NewlyCreated:        " + apiResult.NewlyCreated);
                Console.WriteLine("LastLoginTime:       " + apiResult.LastLoginTime);

                // Information get with the LoginRequestParams
                if (PlayerProfile != null)
                { 
                    // About GetPlayerProfile
                    Console.WriteLine("\nWe get the following information from GetPlayerProfile \n");
                    Console.WriteLine("DisplayName:         " + PlayerProfile.DisplayName);
                    Console.WriteLine("Created:             " + PlayerProfile.Created);
                    Console.WriteLine("ValueToDateInUSD:    " + PlayerProfile.TotalValueToDateInUSD);
                }
                // About GetUserVirtualCurrency
                if (PlayerCurrencies != null)
                {
                    Console.WriteLine("\nWe get the following information from GetUserVirtualCurrency \n");
                    Console.WriteLine("User Currencies own:       " + PlayerCurrencies.Count);

                    foreach (var PlayerCurrency in PlayerCurrencies)
                    {
                        Console.WriteLine("---");
                        Console.WriteLine("Currency:            " + PlayerCurrency.Key);
                        Console.WriteLine("Value:               " + PlayerCurrency.Value);
                    }
                }                
            }
        }
    }
}
