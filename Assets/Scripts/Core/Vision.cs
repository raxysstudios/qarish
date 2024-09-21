using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vision : MonoBehaviour
{
    public UnityEvent<Team> onEnter;
    public UnityEvent<Team> onExit;

    public List<Team> targets;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Team>(out var other)
            && !targets.Contains(other))
        {
            targets.Add(other);
            onEnter.Invoke(other);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Team>(out var other)
            && targets.Contains(other))
        {
            targets.Remove(other);
            onExit.Invoke(other);
        }
    }

    public Team GetClosestTarget()
    {
        var min = float.MaxValue;
        Team result = null;
        foreach (var target in targets)
        {
            var sqrDist = (target.transform.position
                           - transform.position).sqrMagnitude;
            if (sqrDist < min)
            {
                result = target;
                min = sqrDist;
            }
        }
        return result;
    }
}
