# Player Authentication mechanisms in PlayFab
As many players are scared about their privacy and may abandon a game that asks for an e-mail or identifiable information, authentication should use an anonymous login for creating a new account and linking new devices to an existing account. However, once the “anonymous” step is complete, you should provide the option to add recoverable login.

This is usefull for many reasons :

- As a player, you can retrieve your game progress and status when your device change. Many people play from phone and tablet, depending if they are in a couch or toilet.
- As a company, you can track the customer in all your touch point : Website, Forum, Helpdesk, etc. This is the best way to measure your revenue and avoid your player churn or lost.
-- A free account lost forever is a disappointment…
-- A paid account lost forever affects revenue.
So let’s ding into this step customer contact and see how we can do.

## Anonymous Authentication
Anonymous authentication is the process of confirming a user’s right to access a resource. 
Unlike traditional authentication, which may require credentials such as a username and password, anonymous authentication allows users to log in to the system without exposing their actual identity. 
The most crucial benefit to anonymous authentication is the preservation of personal safety and security when conducting business online. 
This involves both personal and professional considerations, but the main point is protecting the user’s identity on the Internet, along with preventing other individuals from having the ability to track and identify users online.

Hopefully, and because we need to measure the ARPU, there is some mechanisms that allow us to provide an anonymous but persistent authentication. They require zero input from the player, so there’s no friction to the first time user experience – and the result is a unique account for each player.

- Authenticate Player based on computer information
-- Login on a computer: LoginWithCustomID
- Authenticate Player based on the Device ID (Serial number of the device)
-- Login on an IOS Device: LoginWithIOSDeviceID
-- Login on an Android Device: LoginWithAndroidDeviceID

To understand mechanism and code related, follow theses examples:

[Login in Console](./Console/)
Login in WebApp
Login in Unity

## Recoverable login mechanisms
Recoverable login mechanisms require some identity information from the player. Player must either authenticate with an external provider (e.g. Facebook, iOS, Google, Kongregate, PlayStation, Steam, Xbox Live, etc.), or manage the login directly within PlayFab, by using either a user name or email address and password.
