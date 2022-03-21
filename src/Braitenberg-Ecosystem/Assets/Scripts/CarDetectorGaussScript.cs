using System;
using Assets.Scripts;

public class CarDetectorGaussScript : CarDetectorScript
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
    
    /// <summary>
    /// Method that gets the sensors output(for cars/gauss).
    /// </summary>
    /// <returns>Returns the output .</returns>
    public override float GetOutput()
        => _outputManipulator.ManipulateOutput(output);
}
