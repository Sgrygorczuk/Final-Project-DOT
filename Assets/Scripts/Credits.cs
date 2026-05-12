using UnityEngine;
using UnityEngine.SceneManagement; // THIS LINE WAS MISSING

public class Credits : MonoBehaviour
{
    [Header("Option A: Scene Loading")]
    public string creditsSceneName = "CreditsScene";

    [Header("Option B: Panel Toggling")]
    public GameObject creditsPanel;

    // Use this if your Credits are in a different Scene
    public void LoadCreditsScene()
    {
        // Now SceneManager will be recognized!
        SceneManager.LoadScene(creditsSceneName);
    }

    // Use this to show a pop-up panel on the same screen
    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true);
        }
    }

    // Use this for a "Close" or "Back" button on the credits panel
    public void CloseCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}