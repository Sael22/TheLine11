using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    // Reference to the button (optional, but can be linked in the Inspector)
    public Button quitButton;

    void Start()
    {
        // Ensure the button is linked
        if (quitButton != null)
        {
            // Add listener to the button click event
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    // This function will be called when the button is clicked
    void QuitGame()
    {
        // Log a message (optional, helpful for debugging)
        Debug.Log("Quit Game");

        // Quit the application
        Application.Quit();

        // If running in the editor (for testing purposes), stop play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
