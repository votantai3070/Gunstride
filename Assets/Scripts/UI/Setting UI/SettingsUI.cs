using UnityEngine;
using UnityEngine.Audio;

public class SettingsUI : MonoBehaviour
{
    private const string BGM_VOLUME = "BGMVolume";
    private const string SFX_VOLUME = "SFXVolume";

    private const float VOLUME_ON = 0f;
    private const float VOLUME_OFF = -80f;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private ToggleBtn musicToggle;

    [SerializeField] private ToggleBtn sfxToggle;

    [SerializeField] private ToggleBtn vibrationToggle;

    private bool isMusic = true;
    private bool isSound = true;

    private void Start()
    {
        ApplyAudioSettings();
        GenerateToggle();
    }

    private void OnEnable()
    {
        isMusic = PlayerPrefs.GetInt("BGM", 1) == 1;
        isSound = PlayerPrefs.GetInt("SFX", 1) == 1;
        GenerateToggle();
    }

    private void GenerateToggle()
    {
        musicToggle?.SetToggle(isMusic);
        sfxToggle?.SetToggle(isSound);
        vibrationToggle.SetToggle(VibrationManager.IsEnabled);
    }

    public void SetMusicToggle()
    {
        isMusic = !isMusic;

        PlayerPrefs.SetInt("BGM", isMusic ? 1 : 0);
        PlayerPrefs.Save();

        SetMixerVolume(BGM_VOLUME, isMusic);
        musicToggle?.SetToggle(isMusic);
    }

    public void SetSFXToggle()
    {
        isSound = !isSound;

        PlayerPrefs.SetInt("SFX", isSound ? 1 : 0);
        PlayerPrefs.Save();

        SetMixerVolume(SFX_VOLUME, isSound);
        sfxToggle?.SetToggle(isSound);
    }

    public void SetVibrationToggle()
    {
        bool newValue = !VibrationManager.IsEnabled;

        VibrationManager.SetEnabled(newValue);

        vibrationToggle?.SetToggle(newValue);

        // Rung thử khi người chơi bật setting.
        if (newValue)
            VibrationManager.Vibrate();
    }

    private void ApplyAudioSettings()
    {
        SetMixerVolume(BGM_VOLUME, isMusic);
        SetMixerVolume(SFX_VOLUME, isSound);
    }

    private void SetMixerVolume(string parameterName, bool isEnabled)
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer chưa được gán.", this);
            return;
        }

        float volume = isEnabled ? VOLUME_ON : VOLUME_OFF;

        StartCoroutine(FadeMixerVolume(parameterName, volume, fadeDuration));
    }

    private System.Collections.IEnumerator FadeMixerVolume(string parameterName, float targetVolume, float duration)
    {
        audioMixer.GetFloat(parameterName, out float currentVolume);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float volume = Mathf.Lerp(currentVolume, targetVolume, elapsed / duration);

            audioMixer.SetFloat(parameterName, volume);

            yield return null;
        }

        audioMixer.SetFloat(parameterName, targetVolume);
    }
}