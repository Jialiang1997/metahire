# MetaHire Documentation

MetaHire is an online multiplayer platform that allows players to connect in a common
space. The game uses the PlayFab API, which allows users to create an account to the
backend database. The game contains functionality like voice chat and texting. Users can use
the game as a platform to gain recruiting insights and the chance for mock interviews. The
game is currently still a prototype, but more design elements are expected to be included in the
future.

Meta Hire is programmed with Unity version 2021.3.6f1.

---

## Structure

The technical structure is based on photon unity
user ID and time log.
networking Pun which is a Unity
package for multiplayer games. It has Flexible matchmaking to get players into game rooms
where objects can be synced over the network.
It implemented three APIs for online multiplayer functionality and they are photon server,
photon chat, and photon voice. The photon server api provides a basic running environment for
multiplayer platforms, and for the current service plan, this platform is capable of 5-10 players
per gaming lobby and 5 lobbies maximum. We can increase this volume any time by changing
the service plan.
For the photon chat API, it is an easy way to add chat functionality to apps or games.
With this implementation, the users can communicate in the game and view the chat history with
Photon voice allows to attach an audio source to an object in unity so that users can
freely place the audio streams.
 
In addition, there is another API that was applied for online networking based on the
Playfab Platform. Compared to the previous version, the application of PlayFab API allows
users to better manage their account and instead of using a one-time user ID, this platform
allows users to create a permanent user ID within the registration process.
The advantage of using this modular- based technical structure is that since it is using
separate APIs it is very easy for developers to debug or add functionalities separately. For
example, in the future if developers want to attach a user database to this application, all they
need to do is to implement another API such as AWS S3 database and programming that
separately.

## User Flow

The Meta Hire application consists of three scenes, and they are Menu Scene,
Login/Register Scene and Game Scene.
As the user launches the app, it will firstly generate a welcome screen with title Logo and
a brief introduction to proceed to the next scene. After entering the Login Scene, it will initiate
the options of login to the game or register a game account with PlayFab platform. If the user
successfully logs into the game, it will automatically launch the game scene. In the game scene
it will generate a UI first to let the user choose their virtual avatar. After the confirmation, the
user will spawn into the virtual zone with the chosen avatar and they are able to interact with all
the functional UI and objects such as communicating with the chat box and pressing ‘e’ to enter
the voice chat. In addition, the users can always go back to the main menu and disconnect from
the server anytime in the game by pressing the exit button.

## Scripts

AudioManager - The management file for the game sounds effects and background music.
BacktoMenu - The scene controller for exit to the homescreen.
Blur - The blur effect of the homescreen and some UI scene background.
ChatControl - The management of Photon Chat API and it controls the chat box in game.
EnterRoomControl - The controller of the Photon Network package, and it will initiate the game
lobby and register the room.
GameManager - the mono script of game manager.
LobbyControl - The controller for the lobby list UI.
LoginControl - Associated with the PlayFab API to control the user login.
MenuNetworkChecker - To test the connection of Photon Network in the menu and make sure it
disconnected at the menu scene.
NetworkLauncher - The launcher of the PlayFab network.
NPCManager - UI manager for letting users select their virtual avatar.
PlayerController - The management of player instances and it will initiate the rigidbody and
some other physical reaction in game.
RegisterControl - The controller of the registration UI and it will initiate the check for invailid
input.
RoomItemControl - Controller of the lobby list.
sceneloader - The management of multiple scene switching.
UIManager - The testing of the Photon Network and it will initiate the game scene UI.
VoiceTrigger - The launcher of Photon Voice functionality, and it will check if the user satisfies
