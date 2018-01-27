using System;

[Flags]
public enum Reward
{
    None = 0,
    Move = 1,
    Dig = 2,
    Build = 4,
    MoveX2 = 8,
    DigX2 = 16,
    BuildX2 = 32
}