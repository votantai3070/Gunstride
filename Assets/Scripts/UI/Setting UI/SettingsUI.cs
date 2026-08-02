using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private ToggleBtn musicToggle;
    [SerializeField] private ToggleBtn sfxToggle;
    [SerializeField] private ToggleBtn vibrationToggle;

    private bool isMusic = true;
    private bool isSound = true;
    private bool isVibration = true;

    private void Start()
    {
        GenerateToggle();
    }

    private void GenerateToggle()
    {
        musicToggle?.SetToggle(isMusic);
        sfxToggle?.SetToggle(isSound);
        vibrationToggle?.SetToggle(isVibration);
    }

    public void SetMusicToggle()
    {
        if (musicToggle != null)
        {
            isMusic = !isMusic;
            musicToggle.SetToggle(isMusic);
        }
    }

    public void SetSFXToggle()
    {
        if (sfxToggle != null)
        {
            isSound = !isSound;
            sfxToggle.SetToggle(isSound);
        }
    }

    public void SetVibrationToggle()
    {
        if (vibrationToggle != null)
        {
            isVibration = !isVibration;
            vibrationToggle.SetToggle(isVibration);
        }
    }
}
