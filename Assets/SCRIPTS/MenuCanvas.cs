using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject settingsPanel;

                     private GameObject musicPlayer;
                     private GameObject menuCamera;
    [Space(12)]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider cameraSlider;
    [Space(12)]
    [SerializeField] private Text settingsText;
    [SerializeField] private Text feedbackTex;
    [SerializeField] private Text levelsText;
    [Space(20)]
    [SerializeField] private Button[] buttonsOfLevels = new Button[19];

    private AudioSource audioSource;
    private Camera mainMenuCamera;


    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;


    private void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Russian)
        {
            settingsText.text = "Настройки".ToString();
            levelsText.text = "Уровни".ToString();

            feedbackTex.text = "Обратная связь:GyulovGames@yandex.ru".ToString();
        }
        else
        {
            settingsText.text = "Settings".ToString();
            levelsText.text = "Levels".ToString();

            feedbackTex.text = "Feedback:GyulovGames@yandex.ru".ToString();
        }

        GetLoad();
    }

    private void GetLoad()
    {
        soundsSlider.value = YandexGame.savesData.soundsVolume;

        musicPlayer = GameObject.FindGameObjectWithTag("Music Player");
        audioSource = musicPlayer.GetComponent<AudioSource>();
        audioSource.volume = YandexGame.savesData.musicVolume;
        musicSlider.value = YandexGame.savesData.musicVolume;

        menuCamera = GameObject.FindGameObjectWithTag("Menu Camera");
        mainMenuCamera = menuCamera.GetComponent<Camera>();
        mainMenuCamera.orthographicSize = YandexGame.savesData.cameraSize;
        cameraSlider.value = YandexGame.savesData.cameraSize;
    }

    public void OpenLevelsPanel()
    {
        levelsPanel.SetActive(true);
        int currentCompletedLevels = YandexGame.savesData.completedLevels;
        
        for(int i = 0; i < currentCompletedLevels; i++)
        {
            buttonsOfLevels[i].interactable = true;
        }
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void CloseOpenedMenues()
    {
        levelsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        YandexGame.SaveProgress();
    }

    public void MusicSliderFunction()
    {
        float curentMusicVolume = musicSlider.value;
        audioSource.volume = curentMusicVolume;
        YandexGame.savesData.musicVolume = curentMusicVolume;
    }

    public void SoundsSliderFunction()
    {
        float curentSoundsVolume = soundsSlider.value;
        YandexGame.savesData.soundsVolume = curentSoundsVolume;
    }

    public void CameraSliderFunction()
    {
        float curentCameraSize = cameraSlider.value;
        mainMenuCamera.orthographicSize = cameraSlider.value;
        YandexGame.savesData.cameraSize = curentCameraSize;
    }
}
