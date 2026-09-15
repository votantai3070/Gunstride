using UnityEngine;

public static class GameColors
{
    // ── Core Fantasy-Gun Palette ────────────
    // Original palette:
    // Moss Green   #7AC74F
    // Willow Green #A1CF6B
    // Pale Amber   #D5D887
    // Golden Sand  #E0C879
    // Vibrant Coral #E87461

    // Extra dark neutral for readable text and dark panels.
    public static readonly Color DarkMoss = HexToColor("#26352B");
    public static readonly Color DarkMossLight = HexToColor("#526044");
    public static readonly Color LightText = HexToColor("#F5F1DE");

    public static readonly Color MossGreen = HexToColor("#7AC74F");
    public static readonly Color WillowGreen = HexToColor("#A1CF6B");
    public static readonly Color PaleAmber = HexToColor("#D5D887");
    public static readonly Color GoldenSand = HexToColor("#E0C879");
    public static readonly Color VibrantCoral = HexToColor("#E87461");

    // ── Text ────────────────────────────────
    public static readonly Color TextPrimary = LightText;
    public static readonly Color TextSecondary = PaleAmber;
    public static readonly Color TextMuted = DarkMossLight;
    public static readonly Color TextOnLight = DarkMoss;
    public static readonly Color TextDistance = LightText;

    // ── General UI ──────────────────────────
    public static readonly Color Background = DarkMoss;
    public static readonly Color PanelBackground = HexToColor("#334A3C");
    public static readonly Color PanelLight = PaleAmber;
    public static readonly Color PanelBorder = GoldenSand;

    public static readonly Color ButtonNormal = MossGreen;
    public static readonly Color ButtonHover = WillowGreen;
    public static readonly Color ButtonSelected = GoldenSand;
    public static readonly Color ButtonDisabled = HexToColor("#657560");

    // ── Level Badge ─────────────────────────
    public static readonly Color BadgeBG = DarkMoss;
    public static readonly Color BadgeBorder = GoldenSand;
    public static readonly Color BadgeText = PaleAmber;

    // ── Damage Text ─────────────────────────
    public static readonly Color DamageNormal = LightText;
    public static readonly Color DamageCrit = GoldenSand;
    public static readonly Color DamageDanger = VibrantCoral;

    // ── Elemental Effects ───────────────────
    // Keep elemental colors visually distinct from the fantasy UI palette.
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

    public static readonly Color StatusSuccess = MossGreen;
    public static readonly Color StatusWarning = GoldenSand;
    public static readonly Color StatusDanger = VibrantCoral;
    public static readonly Color StatusDisabled = DarkMossLight;

    // ── Weapon / Gun Fantasy ────────────────
    // Main material: dark metal or dark enchanted wood.
    public static readonly Color WeaponBody = DarkMoss;

    // Secondary mechanical parts, leather or mossy details.
    public static readonly Color WeaponSecondary = MossGreen;

    // Antique metal trims, engravings and rare-item details.
    public static readonly Color WeaponMetal = GoldenSand;

    // Magic core, runes and enchanted glow.
    public static readonly Color WeaponMagic = PaleAmber;

    // Overheat, muzzle flash, damage warning and cursed marks.
    public static readonly Color WeaponDanger = VibrantCoral;

    // ── Rarity ──────────────────────────────
    public static readonly Color RarityCommon = LightText;
    public static readonly Color RarityUncommon = MossGreen;
    public static readonly Color RarityRare = PaleAmber;
    public static readonly Color RarityLegendary = GoldenSand;
    public static readonly Color RarityCursed = VibrantCoral;

    // ── Stats Panel ─────────────────────────
    public static readonly Color StatHeader = DarkMoss;
    public static readonly Color StatLabel = DarkMossLight;
    public static readonly Color StatValue = DarkMoss;
    public static readonly Color StatBuffed = MossGreen;
    public static readonly Color StatDebuffed = VibrantCoral;
    public static readonly Color StatHighlight = GoldenSand;

    // ── Helper ──────────────────────────────
    public static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;

        Debug.LogWarning($"Invalid color hex: {hex}");
        return Color.white;
    }
}