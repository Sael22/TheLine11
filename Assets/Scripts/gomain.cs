using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    // Reference to the button
    public Button mainMenuButton;

    void Start()
    {
        // Ensure the button is linked
        if (mainMenuButton != null)
        {
            // Add listener to the button click event
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
    }

    // This function will be called when the button is clicked
    void OnMainMenuButtonClicked()
    {
        // Load the "MAIN" scene
        // Ensure that "MAIN" is added to the build settings
        SceneManager.LoadScene("MAIN");
    }
}
