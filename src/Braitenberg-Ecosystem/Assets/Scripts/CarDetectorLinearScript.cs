public class CarDetectorLinearScript : CarDetectorScript
{

    public bool Inverse = false;
    public override float GetOutput()
    {
        if (Inverse)
        {
            return -output;
        }
        else
        {
            return output;
        }
    }

    // YOUR CODE HERE

}
