using UnityEngine;

public class PreviewNoise : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public Vector2 aspectRatio;
    public int resolution;
    public bool scalePlaneToFit;

    public NoiseLayer[] noiseLayers;

    private void OnValidate()
    {
        ValidateParameters();

        float[,] noiseMap = ValidateNoiseMap();

        ApplyTexture(noiseMap, scalePlaneToFit);
    }

    private void ApplyTexture(float[,] noise, bool scalePlaneToFit)
    {
        int width = noise.GetLength(0);
        int height = noise.GetLength(1);

        if (scalePlaneToFit)
            meshRenderer.transform.localScale = new Vector3(width, 1, height);

        Color[] textureMap = new Color[noise.Length];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color value = Color.Lerp(Color.black, Color.white, noise[x, y]);
                textureMap[y * width + x] = value;
            }
        }

        Texture2D texture2D = new Texture2D(width, height);

        SetTexture(texture2D, textureMap);
    }

    private void ValidateParameters()
    {
        if (aspectRatio.x < 1 || aspectRatio.y < 1)
        {
            aspectRatio = Vector2.one;
        }

        if (resolution < 1)
        {
            resolution = 1;
        }
    }

    private float[,] ValidateNoiseMap()
    {
        int width = Mathf.RoundToInt(aspectRatio.x * resolution);
        int height = Mathf.RoundToInt(aspectRatio.y * resolution);

        float[,] combinedNoiseMap = new float[width, height];

        for (int i = noiseLayers.Length - 1; i >= 0; i--)
        {
            noiseLayers[i].BlendWithMap(combinedNoiseMap, noiseLayers[i]);
        }

        return combinedNoiseMap;
    }

    private void SetTexture(Texture2D texture2D, Color[] textureMap)
    {
        meshRenderer.sharedMaterial.mainTexture = texture2D;
        texture2D.wrapMode = TextureWrapMode.Clamp;

        texture2D.SetPixels(textureMap);
        texture2D.Apply();
    }
}