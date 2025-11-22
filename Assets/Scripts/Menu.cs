using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject MainCameraObject;

    // Gestion du volume
    public float startVolume = 0.6F; // De base on veut le slider à x%


    public float maxVolume = 30f; // Quand un volume vaut 1, il a son groupe audio à 30dB
    public float minVolume = -50f; // Quand un volume vaut 0, il a son groupe audio à -50dB

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

    public AudioMixer mixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playButton.onClick.AddListener(play);
        settingsButton.onClick.AddListener(settings);
        quitButton.onClick.AddListener(Application.Quit);

        leaveSettingsButton.onClick.AddListener(settings);

        startVolume = Mathf.Lerp(minVolume, maxVolume, startVolume);

        mainVolumeSlider.onValueChanged.AddListener(volumeMainUpdate);
        mainVolumeSlider.minValue = minVolume;
        mainVolumeSlider.maxValue = maxVolume;
        mainVolumeSlider.value = startVolume;

        //settingsPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(play);
        settingsButton.onClick.RemoveListener(settings);
        quitButton.onClick.RemoveListener(Application.Quit);


        leaveSettingsButton.onClick.RemoveListener(settings);
    }

    // Fonction de changement de scène vers le jeu
    public void play()
    {
        MainCameraObject.SetActive(false);
        backgroundPanel.SetActive(false);
        SceneManager.LoadScene("ClementScene", LoadSceneMode.Additive);
    }

    public void settings()
    {
        if (backgroundPanel.activeSelf)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        else
        {
            backgroundPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settings();
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void volumeMainUpdate(float newVolume)
    {
        // volumeMixerUpdate(newVolume, , "mainVolumeParam");
    }

    public void volumeMixerUpdate(float newVolume, string parameterName)
    {
        newVolume = Mathf.Lerp(minVolume, maxVolume, newVolume);
        mixer.SetFloat(parameterName, newVolume);
    }

}
