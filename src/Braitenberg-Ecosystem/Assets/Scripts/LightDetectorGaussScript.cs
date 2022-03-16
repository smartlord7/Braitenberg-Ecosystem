using System;

public class LightDetectorGaussScript : LightDetectorScript
{

    public float stdDev = 20;
    public float mean = 10.0f;
    public bool inverse = false;


    // Get gaussian output value
    public override float GetOutput()
    {
        float outputActivated = 0;

        if (!ApplyThresholds || ThresholdMin <= output && output <= ThresholdMax)
        {
            outputActivated = (float) Math.Exp(-0.5 * Math.Pow(output - mean, 2) / Math.Pow(stdDev, 2));
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


        return outputActivated;
    }
}
