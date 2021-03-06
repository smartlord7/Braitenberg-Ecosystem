using System;
using UnityEngine;

public class BallBehaviourScript : MonoBehaviour
{

    /// <summary>
    /// Method that clones balls upon hit.
    /// </summary>
    /// <param name="collision">The object the ball hit</param>
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
                var mat = Resources.Load("T_02_Diffuse", typeof(Material)) as Material;
                newObj.GetComponent<Renderer>().material = mat;
                newObj.GetComponent<Renderer>().material.SetColor("_Color", new Color(Math.Abs(x) / 20, y, Math.Abs(z) / 20));
            }
        }
    }
}
