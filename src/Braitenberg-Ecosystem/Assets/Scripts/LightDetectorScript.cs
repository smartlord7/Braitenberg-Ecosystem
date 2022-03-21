using System;
using System.Collections;
using UnityEngine;

public class LightDetectorScript : MonoBehaviour
{
    public float angle = 360;
    public bool ApplyThresholds, ApplyLimits;
    public float LimitMin, LimitMax;
    public float ThresholdMin, ThresholdMax;
    public bool Negative = false;
    public bool Inverse = false;
    private bool useAngle = true;

    public float output;
    public int numObjects;

    void Start()
    {
        output = 0;
        numObjects = 0;

        if (angle > 360)
        {
            useAngle = false;
        }
    }

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

    public virtual float GetOutput() { throw new NotImplementedException(); }

    GameObject[] GetAllLights()
    {
        return GameObject.FindGameObjectsWithTag("Light");
    }

    GameObject[] GetVisibleLights()
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
