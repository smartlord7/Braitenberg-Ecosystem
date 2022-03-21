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

    public override float GetOutput()
        => _outputManipulator.ManipulateOutput(output);
}
