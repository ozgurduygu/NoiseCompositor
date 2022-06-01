using UnityEngine;

public struct NoiseMapTransform
{
    public readonly int width;
    public readonly int height;
    public readonly Vector2 offset;
    public readonly float halfWidth;
    public readonly float halfHeight;

    public NoiseMapTransform(int width, int height, Vector2 offset, int seed)
    {
        this.width = width;
        this.height = height;
        this.halfWidth = width / 2;
        this.halfHeight = height / 2;

        System.Random random = new System.Random(seed);

        this.offset = new Vector2(random.Next(-100000, 100000) + offset.x, random.Next(-100000, 100000) - offset.y);
    }
}