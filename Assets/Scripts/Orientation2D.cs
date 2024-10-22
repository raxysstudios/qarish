using System;
using UnityEngine;

public class Orientation2D : MonoBehaviour
{
    public Transform root;

    public float Sign
    {
        get => Mathf.Sign(transform.lossyScale.x);
        set
        {
            if (Mathf.Sign(Sign * value) < 0)
                transform.localScale *= new Vector2(-1, 1);
        }
    }

    public Vector3 Right => root.right;
    public Vector3 Center => root.position;

    void Awake()
    {
        if (root == null)
            root = transform;
    }

    public void TurnTo(Vector3 point)
    {
        Sign = point.x - transform.position.x;
    }

    public void LookAt(Vector3 point)
    {
        var angle = Vector2.SignedAngle(
            Sign * Vector3.right,
            point - root.position
        );
        root.localEulerAngles = angle * Sign * Vector3.forward;
    }
}
