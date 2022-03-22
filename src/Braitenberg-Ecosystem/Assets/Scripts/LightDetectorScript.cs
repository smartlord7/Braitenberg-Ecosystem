using System;
using System.Collections;
using UnityEngine;

public class LightDetectorScript : MonoBehaviour
{
    private bool useAngle = true;
    public float angle = 360;
    public bool ApplyThresholds, ApplyLimits;
    public float LimitMin, LimitMax;
    public float ThresholdMin, ThresholdMax;
    public bool Negative = false;
    public bool Inverse = false;
    public float output;
    public int numObjects;

    /// <summary>
    /// Method that starts up the light detector sensors.
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
    /// Method that updates the light detector sensors.
    /// </summary>
    void Update()
    {
        GameObject[] lights;

        if (useAngle)
        {
            lights = GetVisibleLights();
        }
        else
        {
            lights = GetAllLights();
        }

        output = 0;
        numObjects = lights.Length;

        foreach (GameObject light in lights)
        {
            float r = light.GetComponent<Light>().range;
            output += 1.0f / ((transform.position - light.transform.position).sqrMagnitude / r + 1);
        }
    }

    /// <summary>
    /// Method that gets the sensors output.
    /// </summary>
    /// <returns>Returns all "Light" tagged objects. The sensor angle is not taken into account.</returns>
    public virtual float GetOutput() { throw new NotImplementedException(); }

    /// <summary>
    /// Method that gets all the lights.
    /// </summary>
    /// <returns>Returns all the lights. </returns>
    private GameObject[] GetAllLights()
    {
        return GameObject.FindGameObjectsWithTag("Light");
    }

    /// <summary>
    /// Method that gets all the visible lights.
    /// </summary>
    /// <returns>Returns all the visible lights. </returns>
    private GameObject[] GetVisibleLights()
    {
        ArrayList visibleLights = new ArrayList();
        float halfAngle = angle / 2.0f;

        GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");

        foreach (GameObject light in lights)
        {
            Vector3 toVector = (light.transform.position - transform.position);
            Vector3 forward = transform.forward;
            toVector.y = 0;
            forward.y = 0;
            float angleToTarget = Vector3.Angle(forward, toVector);

            if (angleToTarget <= halfAngle)
            {
                visibleLights.Add(light);
            }
        }

        return (GameObject[])visibleLights.ToArray(typeof(GameObject));
    }
}
