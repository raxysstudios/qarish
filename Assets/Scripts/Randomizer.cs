using EasyButtons;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public float minWidth;
    public float maxWidth;

    [Space(10)]
    public float minHeight;
    public float maxHeight;

    [Space(10)]
    public Gradient color;
    public Sprite[] sprites;

    [Button]
    public void Randomize()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = color.Evaluate(Random.value);
        if (sprites.Length > 0)
            renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        if (minWidth == maxWidth || minHeight == maxHeight)
            return;
        transform.localScale = new Vector3(
            Random.Range(minWidth, maxWidth),
            Random.Range(minHeight, maxHeight),
            1);
    }
}
