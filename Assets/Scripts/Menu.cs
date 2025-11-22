using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public float mainVolume = 0.5F;
    public float musicVolume = 0.5F;
    public float environmentVolume = 0.5F;
    public float sfxVolume = 0.5F;

    public GameObject settingsPanel;


    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    public Button touchUpButton;
    public Button touchLeftButton;
    public Button touchDownButton;
    public Button touchRightButton;

    public Button touchInteract;
    public Button touchJump;
    public Button touchPlace;

    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider environmentVolumeSlider;
    public Slider sfxVolumeSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playButton.onClick.AddListener(play);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(play);
    }
    public void play()
    {

    }
}
