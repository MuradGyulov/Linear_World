
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

        // Ваши сохранения
        public int completedLevels = 1;
        public float soundsVolume = 0.4f;
        public float musicVolume = 0.14f;
        public float cameraSize = 4.8f;
    }
}
