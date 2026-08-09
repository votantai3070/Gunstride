using System;

[Flags]
public enum SkillUpgradeType
{
    None = 0,

    Single = 1 << 0,
    Pierce = 1 << 1,
    Explode = 1 << 2,
    Bounce = 1 << 3,
    AutoTarget = 1 << 4,
    Chain = 1 << 5,
    Chill = 1 << 6,
    Stun = 1 << 7,
    Burn = 1 << 8,
}