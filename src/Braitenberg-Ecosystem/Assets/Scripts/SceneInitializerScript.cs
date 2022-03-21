using System;
using UnityEngine;

public class SceneInitializerScript : MonoBehaviour
{
    public Vector3 max;
    public Vector3 min;
    public GameObject[] generatedObjects;
    public int[] generatedObjectsOcurrences;

    private void GenerateScene()
    {
        for (int j = 0; j < generatedObjects.Length; j++)
        {
            GameObject obj = generatedObjects[j];
            int numObjects = generatedObjectsOcurrences[j];

            for (int i = 0; i < numObjects; i++)
            {
                float x = UnityEngine.Random.Range(min.x, max.x);
                float y = UnityEngine.Random.Range(min.y, max.y);
                float z = UnityEngine.Random.Range(min.z, max.z);
                float angle = UnityEngine.Random.Range(0, Mathf.PI);
                float angleDegrees = -angle * Mathf.Rad2Deg;

                Vector3 pos = new Vector3(x, y, z);
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

                var genObj = Instantiate(obj, pos, rot);
                if (genObj.name.Contains("Ball"))
                {
                    var mat = Resources.Load("T_02_Diffuse", typeof(Material)) as Material;
                    genObj.GetComponent<Renderer>().material = mat;
                    genObj.GetComponent<Renderer>().material.SetColor("_Color", new Color(Math.Abs(x) / 20, y, Math.Abs(z) / 20));
                }
            }
        }
    }

    void Start()
    {
        Cursor.visible = false;
        GenerateScene();
    }
}
