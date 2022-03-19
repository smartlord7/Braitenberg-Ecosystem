﻿using UnityEngine;

public class CarBehaviour : MonoBehaviour
{

    public float MaxSpeed;
    public WheelCollider RR;
    public WheelCollider RL;
    public bool DetectLights = true;
    public bool DetectCars = false;
    public bool DirectLightSensorsConnection = false;
    public bool DirectCarSensorsConnection = false;
    public LightDetectorScript RightLD;
    public LightDetectorScript LeftLD;
    public CarDetectorScript LeftCD;
    public CarDetectorScript RightCD;
    public float currentScale = 1.0f;
    public float maxScale = 1.5f;
    public float scaleMult = 1.1f;

    private Rigidbody m_Rigidbody;
    public float m_LeftWheelSpeed;
    public float m_RightWheelSpeed;
    private float m_axleLength;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_axleLength = (RR.transform.position - RL.transform.position).magnitude;
    }

    void FixedUpdate()
    {
        //Calculate forward movement
        float targetSpeed = (m_LeftWheelSpeed + m_RightWheelSpeed) / 2;
        Vector3 movement = transform.forward * targetSpeed * Time.fixedDeltaTime;

        //Calculate turn degrees based on wheel speed
        float angVelocity = (m_LeftWheelSpeed - m_RightWheelSpeed) / m_axleLength * Mathf.Rad2Deg * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, angVelocity, 0.0f);

        //Apply to rigid body
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    public void OnCollisionEnter(Collision collision)
    {
        var other = collision.rigidbody;

        if (other?.name.Contains("Ball") ?? false)
        {
            float carVelocity = (m_Rigidbody.velocity).magnitude;
            float ballVelocity = (other?.velocity)?.magnitude ?? -1;

            if (ballVelocity == -1)
            {
                return;
            }

            if (carVelocity > ballVelocity + 0.2)
            {
                other.gameObject.SetActive(false);

                if (currentScale < maxScale)
                {
                    m_Rigidbody.transform.localScale *= scaleMult;
                    currentScale *= scaleMult;
                }
                else
                {
                    m_Rigidbody.transform.localScale = maxScale * Vector3.one;
                }
            }
        }
    }
}
