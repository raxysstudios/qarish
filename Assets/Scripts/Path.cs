using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Path : MonoBehaviour
{
    public List<Path> endLinks;
    public List<Path> startLinks;

    private SplineContainer container;

    void Awake()
    {
        container = GetComponent<SplineContainer>();
    }
}
