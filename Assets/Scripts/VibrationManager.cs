using UnityEngine;

public static class VibrationManager
{
    private const string VIBRATION_KEY = "VibrationEnabled";

    public static bool IsEnabled
    {
        get
        {
            return PlayerPrefs.GetInt(VIBRATION_KEY, 1) == 1;
        }
    }

    public static void SetEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(VIBRATION_KEY, enabled ? 1 : 0);

        PlayerPrefs.Save();
    }

    public static void Vibrate()
    {
        if (!IsEnabled)
            return;

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }
}