using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    public List<SheepControl> sheep;

    [SerializeField]
    private Transform beacon;

    private Transform target;


    public void Stop()
    {
        target = null;
    }

    public void Move(Vector2 point)
    {
        beacon.position = point;
        target = beacon;
    }

    public void Follow(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null)
        {
            foreach (var s in sheep)
                s.ai.destination = s.rb.position;
            return;
        }

        foreach (var s in sheep)
            s.ai.destination = target.position;
    }
}
