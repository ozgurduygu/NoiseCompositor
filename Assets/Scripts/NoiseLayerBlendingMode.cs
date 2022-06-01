using UnityEngine;

public class BlendOperator
{
    public float BlendTwoPixels(NoiseLayer.BlendingModeOption blendingMode, float bgValue, float value)
    {
        switch (blendingMode)
        {
            case (NoiseLayer.BlendingModeOption.Add):
                return Add.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Subtract):
                return Subtract.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Darken):
                return Darken.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Multiply):
                return Multiply.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.ColorBurn):
                return ColorBurn.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.LinearBurn):
                return LinearBurn.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Lighten):
                return Lighten.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Screen):
                return Screen.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.ColorDodge):
                return ColorDodge.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Overlay):
                return Overlay.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.SoftLight):
                return SoftLight.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.HardLight):
                return HardLight.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.VividLight):
                return VividLight.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.LinearLight):
                return LinearLight.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.PinLight):
                return PinLight.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.HardMix):
                return HardMix.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Difference):
                return Difference.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Exclusion):
                return Exclusion.Blend(bgValue, value);

            case (NoiseLayer.BlendingModeOption.Divide):
                return Divide.Blend(bgValue, value);

            default:
                return value;
        }
    }
}

public abstract class BlendingMode
{
    public virtual float Blend(float a, float b)
    {
        return 0f;
    }
}

public class Add : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return a + b;
    }
}

public class Subtract : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return a - b;
    }
}

public class Darken : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return Mathf.Min(a, b);
    }
}

public class Multiply : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return a * b;
    }
}

public class ColorBurn : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return 1.0f - (1.0f - a) / b;
    }
}

public class LinearBurn : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return a + b - 1;
    }
}

public class Lighten : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return Mathf.Max(a, b);
    }
}

public class Screen : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return 1 - (1 - a) * (1 - b);
    }
}

public class ColorDodge : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return b / (1.0f - a);
    }
}

public class Overlay : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        if (a > .5f)
        {
            return Screen.Blend(a, 2 * b - 1);
        }
        else
        {
            return Multiply.Blend(a, 2 * b);
        }
    }
}

public class SoftLight : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return (1 - a) * Multiply.Blend(a, b) + a * Screen.Blend(a, b);
    }
}

public class HardLight : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return (1 - a) * Multiply.Blend(a, b) + a * Screen.Blend(a, b);
    }
}

public class VividLight : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return Overlay.Blend(b, a);
    }
}

public class LinearLight : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        if (b > 0.5f)
        {
            return a / (2 * Mathf.Max(0, (1 - b)));
        }
        else
        {
            return 1 - (1 - a) / (2 * b);
        }
    }
}

public class PinLight : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        if (b < 2 * a - 1)
        {
            return Darken.Blend(a, b);
        }
        else if (b > 2 * a)
        {
            return Lighten.Blend(a, b);
        }
        else
        {
            return b;
        }
    }
}

public class HardMix : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        if (a > 1 - b)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

public class Difference : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return Mathf.Abs(a - b);
    }
}

public class Exclusion : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return a + b - 2 * a * b;
    }
}

public class Divide : BlendingMode
{
    public static new float Blend(float a, float b)
    {
        return a / b;
    }
}