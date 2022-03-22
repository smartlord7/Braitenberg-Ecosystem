using System;
using System.Collections;
using UnityEngine;

public class CarDetectorScript : MonoBehaviour
{
    private bool useAngle = true;
    public float angle = 360;
    public bool ApplyThresholds, ApplyLimits;
    public float LimitMin, LimitMax;
    public float ThresholdMin, ThresholdMax;
    public float output;
    public int numObjects;
    public bool Inverse;
    public bool Negative;

    /// <summary>
    /// Method that starts up the car detector sensors.
    /// </summary>
    void Start()
    {
        output = 0;
        numObjects = 0;

        if (angle > 360)
        {
            useAngle = false;
        }
    }

    /// <summary>
    /// Method that updates the car detector sensors.
    /// </summary>
    void Update()
    {

        GameObject[] Cars;
        float minDist = float.MaxValue;
        GameObject closestCar = null;

        output = 0;
        if (useAngle)
        {
            Cars = GetVisibleCars();
        }
        else
        {
            Cars = GetAllCars();
        }

        if (Cars != null)
        {

            numObjects = Cars.Length;

            foreach (GameObject car in Cars)
            {
                float currDist = (transform.position - car.transform.position).magnitude;

                if (currDist < minDist)
                {
                    minDist = currDist;
                    closestCar = car;
                }
            }

            if (closestCar != null)
            {
                output = 1.0f / ((transform.position - closestCar.transform.position).magnitude + 1);
            }

        }
    }

    /// <summary>
    /// Method that gets the sensors output.
    /// </summary>
    /// <returns>Returns all "Light" tagged objects. The sensor angle is not taken into account.</returns>
    public virtual float GetOutput() { throw new NotImplementedException(); }

    /// <summary>
    /// Method that gets all the cars.
    /// </summary>
    /// <returns>Returns all the cars. </returns>
    private GameObject[] GetAllCars()
    {
        return GameObject.FindGameObjectsWithTag("CarToFollow");
    }

    /// <summary>
    /// Method that gets all the visible cars.
    /// </summary>
    /// <returns>Returns all the visible cars. </returns>
    private GameObject[] GetVisibleCars()
    {
        ArrayList visiblecars = new ArrayList();
        float halfangle = angle / 2.0f;

        GameObject[] Cars = GameObject.FindGameObjectsWithTag("CarToFollow");

        foreach (GameObject car in Cars)
        {
            Vector3 tovector = (car.transform.position - transform.position);
            Vector3 forward = transform.forward;
            tovector.y = 0;
            forward.y = 0;

            float angletotarget = Vector3.Angle(forward, tovector);

            if (angletotarget <= halfangle)
            {
                visiblecars.Add(car);
            }
        }
        return (GameObject[])visiblecars.ToArray(typeof(GameObject));
    }
}
