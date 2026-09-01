using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.root.GetComponentInChildren<UI_FadeScreen>().FadeIn();
    }

    private void OnEnable()
    {
        AudioManager.Instance.StartBGM("playlist_mainMenu");
    }
}
