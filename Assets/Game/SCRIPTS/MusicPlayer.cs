using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer musicPlayerInstance;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if(musicPlayerInstance == null)
        {
            musicPlayerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
