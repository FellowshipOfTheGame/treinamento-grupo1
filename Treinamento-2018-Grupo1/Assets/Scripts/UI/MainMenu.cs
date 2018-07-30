using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script do menu principal.
[AddComponentMenu("Scripts/UI/Main Menu")]
public class MainMenu : MonoBehaviour {

    // Lista dos menus.
    public MenuGroup[] menuGroups;

    [Header("Opções:")]

    [Header("- Resolução:")]
    // Dropdown das resoluções.
    public Dropdown resolutionDropdown;
    // Toggle de tela cheia.
    public Toggle fullscreenToogle;

    [Header("- Áudio:")]
    // Slider do volume principal.
    public Slider masterVolumeSlider;
    // Slider do volume da música
    public Slider musicVolumeSlider;

    public void ToggleMenu (string id) {

        for (int i = 0; i < menuGroups.Length; i++)
            if (id == menuGroups[i].id)
                menuGroups[i].groupObject.SetActive(true);
            else
                menuGroups[i].groupObject.SetActive(false);

    }

    public void LoadLevel (string levelName) {

        SceneManager.LoadScene(levelName);

    }

    public void ExitGame() {

        Application.Quit();
        Debug.Log("Application.Quit()");

    }
}
