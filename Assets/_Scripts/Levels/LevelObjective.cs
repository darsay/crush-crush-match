using System;

public enum ObjectiveType
{
    Red,
    Green,
    Blue,
    Yellow,
    Purple,
    Heart,
    HeartBaloon
}


[Serializable]
public class LevelObjective
{
    public ObjectiveType Type;
    public int Goal;
}
