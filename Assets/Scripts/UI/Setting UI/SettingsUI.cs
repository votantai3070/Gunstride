using UnityEngine;
using UnityEngine.Audio;

public class SettingsUI : MonoBehaviour
{
    private const string BGM_VOLUME = "BGMVolume";
    private const string SFX_VOLUME = "SFXVolume";

    private const float VOLUME_ON = 0f;
    private const float VOLUME_OFF = -80f;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private ToggleBtn musicToggle;

    [SerializeField]
    private ToggleBtn sfxToggle;

    [SerializeField]
    private ToggleBtn vibrationToggle;

    private bool isMusic = true;
    private bool isSound = true;
    private bool isVibration = true;

    private void Start()
    {
        ApplyAudioSettings();
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
        isMusic = !isMusic;

        SetMixerVolume(BGM_VOLUME, isMusic);
        musicToggle?.SetToggle(isMusic);
    }

    public void SetSFXToggle()
    {
        isSound = !isSound;

        SetMixerVolume(SFX_VOLUME, isSound);
        sfxToggle?.SetToggle(isSound);
    }

    public void SetVibrationToggle()
    {
        isVibration = !isVibration;
        vibrationToggle?.SetToggle(isVibration);
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

        bool success = audioMixer.SetFloat(parameterName, volume);

        if (!success)
        {
            Debug.LogError(
                $"Không tìm thấy exposed parameter: {parameterName}",
                this
            );
        }
    }
}