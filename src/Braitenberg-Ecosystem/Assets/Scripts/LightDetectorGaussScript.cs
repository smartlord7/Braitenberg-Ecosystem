using System;
using Assets.Scripts;

public class LightDetectorGaussScript : LightDetectorScript
{
    public float StdDev = 1.0f;
    public float Mean = 0.0f;

    private DetectorOutputManipulator _outputManipulator
    {
        get
        {
            return new DetectorOutputManipulator(
                Negative,
                Inverse,
                ApplyLimits,
                ApplyThresholds,
                LimitMin,
                LimitMax,
                ThresholdMin,
                ThresholdMax,
                (output) => (float)Math.Exp(-0.5 * Math.Pow(output - Mean, 2) / Math.Pow(StdDev, 2)));
        }
    }

    public override float GetOutput()
        => _outputManipulator.ManipulateOutput(output);
}
