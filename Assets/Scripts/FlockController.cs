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
        var engagedCount = 1;
        while (true)
        {
            sheep[0].target = Target.position;
            for (var i = 1; i < engagedCount; i++)
                sheep[i].target = sheep[i - 1].rb.position;
            // sheep[i].target = Target.position;

            if (engagedCount < sheep.Count)
                engagedCount++;

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
                s.target = s.rb.position;
    }
}
