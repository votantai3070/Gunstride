using System;

[Flags]
public enum SkillUpgradeType
{
    None = 0,

    Single = 1 << 0,
    MultiSpawn = 1 << 1,
    TripleLane = 1 << 2,
    Pierce = 1 << 3,
    Explode = 1 << 4,
    Bounce = 1 << 5,
    AutoTarget = 1 << 6,
    Chain = 1 << 7,
    Split = 1 << 8,
    Slow = 1 << 9,
    Stun = 1 << 10,
    Burn = 1 << 11,
    Freeze = 1 << 12
}