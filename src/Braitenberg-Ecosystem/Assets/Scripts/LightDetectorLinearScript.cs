using Assets.Scripts;

public class LightDetectorLinearScript : LightDetectorScript
{
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
                (output) => output);
        }
    }

    /// <summary>
    /// Method that gets the sensors output(for lights/linear).
    /// </summary>
    /// <returns>Returns the output .</returns>
    public override float GetOutput()
        => _outputManipulator.ManipulateOutput(output);
}
