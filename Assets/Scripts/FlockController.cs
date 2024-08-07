using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    public List<SheepControl> sheep;

    [SerializeField]
    private Transform beacon;

    private Transform target;
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            if (targetCoroutine != null)
                StopCoroutine(targetCoroutine);
            if (target != null)
            {
                targetCoroutine = UpdateTargets();
                StartCoroutine(targetCoroutine);
            }
        }
    }

    [SerializeField]
    private float followDelay = .5f;

    IEnumerator targetCoroutine;
    readonly List<Vector2> sheepPoints = new();

    public void Stop()
    {
        Target = null;
    }

    public void Move(Vector2 point)
    {
        beacon.position = point;
        Target = beacon;
        Target = Target;
    }

    public void Follow(Transform target)
    {
        Target = target;
        StartCoroutine(UpdateTargets());
    }

    IEnumerator UpdateTargets()
    {
        sheepPoints.Clear();
        while (true)
        {
            sheep[0].ai.destination = target.position;
            for (var i = 1; i < sheepPoints.Count; i++)
                sheep[i].ai.destination = sheepPoints[i - 1];

            if (sheepPoints.Count < sheep.Count)
                sheepPoints.Add(Vector2.zero);
            for (var i = 0; i < sheepPoints.Count; i++)
                sheepPoints[i] = sheep[i].rb.position;

            yield return new WaitForSeconds(followDelay);
        }
    }

    void Update()
    {
        if (Target != null)
        {
            if (beacon != Target)
                beacon.position = Target.position;
        }
        else
            foreach (var s in sheep)
                s.ai.destination = s.rb.position;
    }
}
