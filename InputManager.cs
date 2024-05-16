//using System.Collections;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class InputManager : MonoBehaviour
//{
//    public GameObject[] characterPrefabs; // Array of character prefabs
//    public Transform spawnPoint1;         // Spawn point for player 1
//    public Transform spawnPoint2;         // Spawn point for player 2

//    private int playerCount = 0;           // Track the number of spawned players

//    void Update()
//    {
//        // Detect input to spawn players
//        if (Input.GetKeyDown(KeyCode.P) && playerCount < 1)
//        {
//            SpawnPlayer(1, spawnPoint1);
//        }

//        if (Input.GetKeyDown(KeyCode.X) && playerCount < 2)
//        {
//            SpawnPlayer(2, spawnPoint2);
//        }
//    }

//    void SpawnPlayer(int playerNumber, Transform spawnPoint)
//    {
//        int characterIndex = (playerNumber == 1) ? 0 : 1;

//        GameObject newPlayer = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, spawnPoint.rotation);


//        PlayerActions playerActions = newPlayer.GetComponent<PlayerActions>();
//        if (playerActions != null)
//        {
//            playerActions.SetPlayerNumber(playerNumber);

//            // Set the input device for the player
//            if (playerNumber == 1)
//            {
//                playerActions.SetInputDevice(GetFirstAvailableGamepad()); // Use the first available gamepad for player 1
//            }
//            else if (playerNumber == 2)
//            {
//                playerActions.SetInputDevice(GetNextAvailableGamepad()); // Use the next available gamepad for player 2
//            }
//        }

//        playerCount++;
//    }

//    InputDevice GetFirstAvailableGamepad()
//    {
//        // Get the first available gamepad
//        var gamepads = Gamepad.all;
//        return (gamepads.Count > 0) ? gamepads[0] : Keyboard.current; // Fallback to keyboard if no gamepad is available
//    }

//    InputDevice GetNextAvailableGamepad()
//    {
//        // Get the next available gamepad after the first one
//        var gamepads = Gamepad.all;
//        return (gamepads.Count > 1) ? gamepads[1] : Keyboard.current; // Fallback to keyboard if only one gamepad is available
//    }
//}