using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.StartBGM("playlist_mainMenu");
    }
}
