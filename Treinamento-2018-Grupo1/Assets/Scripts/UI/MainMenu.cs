using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Script do menu principal.
[AddComponentMenu("Scripts/UI/Main Menu")]
public class MainMenu : MonoBehaviour {


    // Lista dos menus.
    public MenuGroup[] menuGroups;

    [Header("Opções:")]

    [Header("- Resolução:")]
    // Dropdown das resoluções.
    public Dropdown resolutionDropdown;
    private List<Resolution> avaliableResolutions;
    // Toggle de tela cheia.
    public Toggle fullscreenToogle;

    [Header("- Áudio:")]
    // Slider do volume principal.
    public Slider masterVolumeSlider;
    // Slider do volume da música
    public Slider musicVolumeSlider;

    public void Start() {

        // Cria uma lista de resoluções disponíveis e adiciona elas nas opções do Dropdown, também detecta qual é a opção de resolução atual.
        Resolution[] resolutions = Screen.resolutions;
        avaliableResolutions = new List<Resolution>();

        // Garante que não sejam adicionadas resoluções inválidas na lista.
        for (int i = 0; i < resolutions.Length; i++) {

            if (resolutions[i].height >= 600 || Screen.height < 600)
                avaliableResolutions.Add(resolutions[i]);

        }

        List<Dropdown.OptionData> resOptions = new List<Dropdown.OptionData>();
        int currentResIndex = 0;

        for(int i = 0; i < avaliableResolutions.Count; i++) {

            Dropdown.OptionData opt = new Dropdown.OptionData();
            opt.text = avaliableResolutions[i].width + "x" + avaliableResolutions[i].height + " (" + avaliableResolutions[i].refreshRate + "Hz)";
            resOptions.Add(opt);

            if (avaliableResolutions[i].width == Screen.width && avaliableResolutions[i].height == Screen.height)
                currentResIndex = i;

        }

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = currentResIndex;

        // Detecta se jogo está em tela cheia e ajeita o Toggle.
        fullscreenToogle.isOn = Screen.fullScreen;

        // Detecta o volume principal do jogo seta ele e ajeita o Slider.

        if (PlayerPrefs.HasKey("masterVolume"))
            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        else {
            PlayerPrefs.SetFloat("masterVolume", 1.0f);
            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        }

        AudioListener.volume = masterVolumeSlider.value;

        // Detecta o volume da música do jogo seta ele e ajeita o Slider.

        if (PlayerPrefs.HasKey("musicVolume"))
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        else {
            PlayerPrefs.SetFloat("musicVolume", 1.0f);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        if(AudioManager.instance != null) {
            AudioManager.instance.volumeMusic = musicVolumeSlider.value;
            AudioManager.instance.mudarVolumeMusic();
        }

    }

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

    public void ChangeScreenResolution() {

        int index = resolutionDropdown.value;
        bool isFullscreen = fullscreenToogle.isOn;

        if(avaliableResolutions[index].height > 600)
            Screen.SetResolution(avaliableResolutions[index].width, avaliableResolutions[index].height, isFullscreen, avaliableResolutions[index].refreshRate);
        else
            Screen.SetResolution(800, 600, isFullscreen, avaliableResolutions[index].refreshRate);

    }

    public void ToggleMasterVolume() {

        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.Save();
        AudioListener.volume = masterVolumeSlider.value;

    }

    public void ToggleMusicVolume() {

        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.Save();
        if (AudioManager.instance != null) {
            AudioManager.instance.volumeMusic = musicVolumeSlider.value;
            AudioManager.instance.mudarVolumeMusic();
        }

    }
}
