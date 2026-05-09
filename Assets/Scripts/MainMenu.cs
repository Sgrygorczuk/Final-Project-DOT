using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing scenes

public class MainMenu : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameSceneName = "GameScene"; // Type the EXACT name of your gameplay scene

    // 1. Called when the Start Button is clicked
    public void StartGame()
    {
        Debug.Log("Starting Game...");
        SceneManager.LoadScene(gameSceneName);
    }

    // 2. Called when the Quit Button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // This works in the actual build (not inside the Unity Editor)
    }

    // 3. Optional: Back to Menu (if you use this script on a 'Game Over' screen)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}