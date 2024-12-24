using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    // Reference to the button
    public Button startButton;

    void Start()
    {
        // Ensure the button is linked
        if (startButton != null)
        {
            // Add listener to the button click event
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
    }

    // This function will be called when the button is clicked
    void OnStartButtonClicked()
    {
        // Load the game scene (assuming the game scene is named "GameScene")
        // Make sure to add "GameScene" to your build settings in Unity
        SceneManager.LoadScene("GameScene");
    }
}
