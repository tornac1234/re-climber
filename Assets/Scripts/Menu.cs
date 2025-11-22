using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Menu : MonoBehaviour
{
    // Gestion du volume
    public float mainVolume = 0.5F;
    public float musicVolume = 0.5F;
    public float environmentVolume = 0.5F;
    public float sfxVolume = 0.5F;

    // UI panels
    public GameObject backgroundPanel;
    public GameObject settingsPanel;

    // Boutons du mainMenu
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    // Boutons des touches de déplacement
    public Button touchUpButton;
    public Button touchLeftButton;
    public Button touchDownButton;
    public Button touchRightButton;

    // Boutons des touches diverses
    public Button touchInteractButton;
    public Button touchJumpButton;
    public Button touchPlaceButton;

    // Bouton de sortie des paramètres
    public Button leaveSettingsButton;

    // Sliders de gestion du volume
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider environmentVolumeSlider;
    public Slider sfxVolumeSlider;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playButton.onClick.AddListener(play);
        settingsButton.onClick.AddListener(settings);
        leaveSettingsButton.onClick.AddListener(settings);

        //settingsPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(play);
        settingsButton.onClick.RemoveListener(settings);
        leaveSettingsButton.onClick.RemoveListener(settings);
    }

    // Fonction de changement de scène vers le jeu
    public void play()
    {
        backgroundPanel.SetActive(false);
    }

    public void settings()
    {
        if (backgroundPanel.activeSelf)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
    

    
}
