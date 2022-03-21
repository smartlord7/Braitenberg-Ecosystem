using System;

public class CarDetectorGaussScript : CarDetectorScript
{
    public float stdDev = 1.0f;
    public float mean = 0.0f;

    public override float GetOutput()
    {
        float outputActivated = 0;

        if (!ApplyThresholds || ThresholdMin <= output && output <= ThresholdMax)
        {
            outputActivated = (float)Math.Exp(-0.5 * Math.Pow(output - mean, 2) / Math.Pow(stdDev, 2));
        }

        if (ApplyLimits)
        {
            if (outputActivated < LimitMin)
            {
                outputActivated = LimitMin;
            }
            else if (outputActivated > LimitMax)
            {
                outputActivated = LimitMax;
            }
        }

        if (Inverse)
        {
            return -outputActivated;
        }
        else
        {
            return outputActivated;
        }
    }
}
