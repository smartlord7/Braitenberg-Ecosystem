public class CarDetectorLinearScript : CarDetectorScript
{
    public override float GetOutput()
    {
        if (ApplyThresholds)
        {
            if (ThresholdMin < output || output > ThresholdMax)
            {
                output = 0.0f;
            }
        }

        if (ApplyLimits)
        {
            if (output < LimitMin)
            {
                output = LimitMin;
            }
            else if (output > LimitMax)
            {
                output = LimitMax;
            }
        }


        if (Inverse)
        {
            return -output;
        }
        else
        {
            return output;
        }
    }
}
