using UnityEngine;

public class BallBehaviourScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody?.name.StartsWith("Ball") ?? false)
        {
            float x = Random.Range(-20, 20);
            float y = Random.Range(0.5f, 0.5f);
            float z = Random.Range(-20, 20);
            float angle = Random.Range(0, Mathf.PI);
            float angleDegrees = -angle * Mathf.Rad2Deg;

            Vector3 pos = new Vector3(x, y, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(collision.rigidbody.gameObject, pos, rot);
        }
    }
}
