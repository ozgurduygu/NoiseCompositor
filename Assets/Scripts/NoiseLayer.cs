using UnityEngine;
using Unity.Mathematics;

[System.Serializable]
public class NoiseLayer
{
    private NoiseGenerator noiseGenerator = new NoiseGenerator();
    public enum NoiseAlgorithm { Perlin, Cellular, CellularOptimized, Crystal, CrystalOptimized };
    public NoiseAlgorithm noiseAlgorithm;

    private BlendOperator blendOperator = new BlendOperator();
    public enum BlendingModeOption
    {
        Normal, Add, Subtract, Darken, Multiply, ColorBurn, LinearBurn,
        Lighten, Screen, ColorDodge, Overlay, SoftLight, HardLight, VividLight,
        LinearLight, PinLight, HardMix, Difference, Exclusion, Divide
    };
    public BlendingModeOption blendingModeOption;

    [Range(0, 1)]
    public float opacity = 1;

    public int seed;

    public float frequency;
    public float amplitude;

    [Range(0, 6)]
    public int octaves;

    public struct OctaveInfo
    {
        public float frequency;
        public float amplitude;
    }

    [Range(0, 1)]
    public float persistence;
    public float lacunarity;

    public Vector2 offset;

    public float[,] BlendWithMap(float[,] backgroundMap, NoiseLayer noiseLayer)
    {
        int width = backgroundMap.GetLength(0);
        int height = backgroundMap.GetLength(1);

        NoiseMapTransform noiseTransform = new NoiseMapTransform(width, height, offset, seed);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = 0;

                OctaveInfo octave;
                octave.frequency = noiseLayer.frequency;
                octave.amplitude = noiseLayer.amplitude;

                float totalAmplitude = 0;

                for (int o = 0; o <= octaves; o++)
                {
                    float2 noisePoint;
                    noisePoint.x = (x - noiseTransform.halfWidth + noiseTransform.offset.x) / noiseTransform.width * octave.frequency;
                    noisePoint.y = (y - noiseTransform.halfHeight - noiseTransform.offset.y) / noiseTransform.width * octave.frequency;

                    value += noiseGenerator.GeneratePixel(noiseAlgorithm, noisePoint, octave);

                    totalAmplitude += octave.amplitude;
                    octave.frequency *= lacunarity;
                    octave.amplitude *= persistence;
                }

                value /= totalAmplitude;
                float bgValue = backgroundMap[x, y];

                value = blendOperator.BlendTwoPixels(blendingModeOption, bgValue, value);

                backgroundMap[x, y] = Mathf.Lerp(backgroundMap[x, y], value, opacity);
            }
        }

        return backgroundMap;
    }
}