public class CarBehaviour2a : CarBehaviour
{

    void LateUpdate()
    {
        // YOUR CODE HERE
        float leftSensor = 0, rightSensor = 0;

        //Read sensor values
        if (DetectLights)
        {
            if (DirectLightSensorsConnection)
            {
                leftSensor = LeftLD.GetOutput();
                rightSensor = RightLD.GetOutput();
            }
            else
            {
                leftSensor = RightLD.GetOutput();
                rightSensor = LeftLD.GetOutput();
            }
        }

        if (DetectCars)
        {
            if (DirectCarSensorsConnection)
            {
                leftSensor = LeftCD.GetOutput();
                rightSensor = RightCD.GetOutput();
            }
            else
            {
                leftSensor = RightCD.GetOutput();
                rightSensor = LeftCD.GetOutput();
            }
        }


        //Calculate target motor values
        m_LeftWheelSpeed = leftSensor * MaxSpeed;
        m_RightWheelSpeed = rightSensor * MaxSpeed;
    }
}
