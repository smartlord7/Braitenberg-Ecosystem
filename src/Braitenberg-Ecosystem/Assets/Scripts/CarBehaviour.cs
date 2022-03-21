using UnityEngine;

public class CarBehaviour : MonoBehaviour
{

    public float MaxSpeed;
    public WheelCollider RR;
    public WheelCollider RL;
    public bool DetectLights = true;
    public bool DetectCars = false;
    public LightDetectorScript RightLD;
    public LightDetectorScript LeftLD;
    public CarDetectorScript LeftCD;
    public CarDetectorScript RightCD;
    public float currentScale = 1.0f;
    public float maxScale = 3.0f;
    public float scaleMult = 1.15f;
    public float m_LeftWheelSpeed;
    public float m_RightWheelSpeed;

    private Rigidbody m_Rigidbody;
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
            float ballVelocity = (other?.velocity)?.magnitude ?? -1;

            if (ballVelocity == -1)
            {
                return;
            }


            other.gameObject.SetActive(false);
            eatObject(other);
            Debug.Log("Ball eaten");
        }
        else if (other?.name.Contains("Variant") ?? false)
        {
            if ((m_Rigidbody.transform.localScale).sqrMagnitude > (other.transform.localScale).sqrMagnitude)
            {
                other.gameObject.SetActive(false);
                eatObject(other);
                Debug.Log("Car eaten");
            }
        }
    }

    private void eatObject(Rigidbody other)
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
