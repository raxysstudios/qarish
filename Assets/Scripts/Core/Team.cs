using UnityEngine;

public enum TeamFlag
{
    Player,
    Enemy,
    Neutral
}

public class Team : MonoBehaviour
{
    public TeamFlag flag;

    public bool CanHit(GameObject target)
    {
        if (target.TryGetComponent<Team>(out var other))
            return CanHit(other);
        return true;
    }

    public bool CanHit(Team other)
    {
        if (other.flag != flag || other.flag == TeamFlag.Neutral)
            return true;
        return false;
    }

    public bool IsEnemy(GameObject target)
    {
        if (target.TryGetComponent<Team>(out var other))
            return IsEnemy(other);
        return false;
    }

    public bool IsEnemy(Team other)
    {
        if (other.flag != flag && other.flag != TeamFlag.Neutral)
            return true;
        return false;
    }
}
