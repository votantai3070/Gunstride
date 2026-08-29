using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.StartBGM("playlist_mainMenu");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("PlainLevel");
    }
}
