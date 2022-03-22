using System;

namespace Assets.Scripts
{
    public class DetectorOutputManipulator
    {
        public bool Negative { get; set; }
        public bool Inverse { get; set; }
        public bool ApplyLimits { get; set; }
        public bool ApplyThresholds { get; set; }
        public float LimitMin { get; set; }
        public float LimitMax { get; set; }
        public float ThresholdMin { get; set; }
        public float ThresholdMax { get; set; }
        public Func<float, float> ActivationFunction { get; set; }

        public DetectorOutputManipulator(bool negative, bool inverse, bool applyLimits, bool applyThresholds,
            float limitMin, float limitMax, float thresholdMin, float thresholdMax, Func<float, float> activationFunction)
        {
            Negative = negative;
            Inverse = inverse;
            ApplyLimits = applyLimits;
            ApplyThresholds = applyThresholds;
            LimitMin = limitMin;
            LimitMax = limitMax;
            ThresholdMin = thresholdMin;
            ThresholdMax = thresholdMax;
            this.ActivationFunction = activationFunction;
        }

        /// <summary>
        /// Method that gets the output after applying activation functions/filters on it.
        /// </summary>
        /// <returns>Returns the manipulated output.</returns>
        public float ManipulateOutput(float output)
        {
            if (Inverse)
            {
                output = 1.0f - output;
            }

            float outputActivated = 0;

            if (!ApplyThresholds || ThresholdMin <= output && output <= ThresholdMax)
            {
                outputActivated = ActivationFunction(output);
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

            if (Negative)
            {
                outputActivated = -outputActivated;
            }

            return outputActivated;
        }
    }
}
