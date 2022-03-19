using System;
using UnityEngine;

public class BallBehaviourScript : MonoBehaviour
{

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody?.name.StartsWith("Ball") ?? false)
        {
            float x = UnityEngine.Random.Range(-20, 20);
            float y = UnityEngine.Random.Range(0.5f, 0.5f);
            float z = UnityEngine.Random.Range(-20, 20);
            float angle = UnityEngine.Random.Range(0, Mathf.PI);
            float angleDegrees = -angle * Mathf.Rad2Deg;

            Vector3 pos = new Vector3(x, y, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

            float value = UnityEngine.Random.Range(0, 100);

            if (0 <= value && value <= 10)
            {
                GameObject newObj = Instantiate(collision.rigidbody.gameObject, pos, rot);
                var mat = Resources.Load("Ball", typeof(Material)) as Material;
                newObj.GetComponent<Renderer>().material = mat;
                newObj.GetComponent<Renderer>().material.SetColor("_Color", new Color(Math.Abs(x) / 20, y, Math.Abs(z) / 20));
            }
        }
        else if (collision.rigidbody?.name.Contains("Variant") ?? false)
        {
            float carVelocityMag = (collision.rigidbody.velocity).magnitude;
            float thisVelocityMag = (_rigidbody?.velocity)?.magnitude ?? -1;

            if (thisVelocityMag == -1)
            {
                return;
            }

            if (carVelocityMag > thisVelocityMag + 0.2)
            {
                gameObject.SetActive(false);
                collision.rigidbody.transform.localScale *= 1.1f;
            }
        }
    }
}
