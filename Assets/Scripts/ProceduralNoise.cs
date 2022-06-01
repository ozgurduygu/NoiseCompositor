using Unity.Mathematics;

public abstract class Noise
{
    public virtual float Generate(float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        return 0f;
    }
}

public class Perlin : Noise
{
    public static new float Generate(float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        return (noise.cnoise(noisePoint) + 1) / 2 * octave.amplitude;
    }
}

public class Cellular : Noise
{
    public static new float Generate(float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        return noise.cellular(noisePoint).x * octave.amplitude;
    }
}

public class CellularOptimized : Noise
{
    public static new float Generate(float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        return noise.cellular2x2(noisePoint).x * octave.amplitude;
    }
}

public class Crystal : Noise
{
    public static new float Generate(float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        return noise.cellular(noisePoint).y * octave.amplitude;
    }
}

public class CrystalOptimized : Noise
{
    public static new float Generate(float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        return noise.cellular2x2(noisePoint).y * octave.amplitude;
    }
}

public class NoiseGenerator
{
    public float GeneratePixel(NoiseLayer.NoiseAlgorithm noiseAlgorithm, float2 noisePoint, NoiseLayer.OctaveInfo octave)
    {
        switch (noiseAlgorithm)
        {
            case (NoiseLayer.NoiseAlgorithm.Perlin):
                return Perlin.Generate(noisePoint, octave);

            case (NoiseLayer.NoiseAlgorithm.Cellular):
                return Cellular.Generate(noisePoint, octave);

            case (NoiseLayer.NoiseAlgorithm.CellularOptimized):
                return CellularOptimized.Generate(noisePoint, octave);

            case (NoiseLayer.NoiseAlgorithm.Crystal):
                return Crystal.Generate(noisePoint, octave);

            case (NoiseLayer.NoiseAlgorithm.CrystalOptimized):
                return CrystalOptimized.Generate(noisePoint, octave);

            default:
                return 0f;
        }
    }
}