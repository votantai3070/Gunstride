using UnityEngine;

public static class GameColors
{
    // ── Text ────────────────────────────────
    public static readonly Color TextDistance = HexToColor("#F5F1DE");

    // ── Level Badge ─────────────────────────
    public static readonly Color BadgeBG = HexToColor("#1A3A6A");
    public static readonly Color BadgeText = HexToColor("#88CCFF");

    // ── Damage Text ─────────────────────────
    public static readonly Color DamageNormal = HexToColor("#F5F5F5");
    public static readonly Color DamageCrit = HexToColor("#FFD700");

    // ── Elemental Effects ───────────────────
    public static readonly Color Chill = HexToColor("#8DEBFF");
    public static readonly Color ChillDark = HexToColor("#3C8DFF");

    public static readonly Color Fire = HexToColor("#FF6A2A");
    public static readonly Color FireDark = HexToColor("#B82E12");

    public static readonly Color Lightning = HexToColor("#FFE14A");
    public static readonly Color LightningDark = HexToColor("#B39B20");

    // ── Status Text ─────────────────────────
    public static readonly Color ChillText = HexToColor("#BDF6FF");
    public static readonly Color FireText = HexToColor("#FFB347");
    public static readonly Color LightningText = HexToColor("#FFF3A3");

    // ── Stats Panel ─────────────────────────
    public static readonly Color StatHeader = HexToColor("#1A0A00");
    public static readonly Color StatLabel = HexToColor("#2E1A0A");
    public static readonly Color StatValue = HexToColor("#8B1A1A");
    public static readonly Color StatBuffed = HexToColor("#E8C840");
    public static readonly Color StatDebuffed = HexToColor("#C8001E");

    // ── Helper ──────────────────────────────
    public static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;

        Debug.LogWarning($"Invalid color hex: {hex}");
        return Color.white;
    }
}