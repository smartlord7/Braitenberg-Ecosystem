public class CarBehaviour2a : CarBehaviour
{
    void LateUpdate()
    {
        float leftSensor = 0;
        float rightSensor = 0;

        //Read sensor values
        if (DetectLights)
        {
            leftSensor = LeftLD.GetOutput();
            rightSensor = RightLD.GetOutput();
        }

        if (DetectCars)
        {
            leftSensor = LeftCD.GetOutput();
            rightSensor = RightCD.GetOutput();
        }

        //Calculate target motor values
        m_LeftWheelSpeed = leftSensor * MaxSpeed;
        m_RightWheelSpeed = rightSensor * MaxSpeed;
    }
}
