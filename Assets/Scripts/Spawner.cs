using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject particle;
    static GameObject node;
    //public GameObject SpringDamper;

    public List<GameObject> particleList = new List<GameObject>();
    public List<GameObject> springdamp = new List<GameObject>();

    public int rows = 5, cols = 5, width, length;

    void Awake()
    {
        node = particle;
        Spawn();
    }

    void Update()
    {
        ApplyGravity();
    }

    public void Spawn()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject o;
                o = Instantiate(node) as GameObject;
                o.transform.position = new Vector3(i * width / rows, j * length / cols, 0);
                particleList.Add(o);
            }
        }

    }

    public void ApplyGravity()
    {
        foreach(GameObject o in particleList)
        {
            Vector3 g = new Vector3(0, -9.8f, 0);
            GetComponent<Particle>().force = GetComponent<Particle>().mass * g;
        }
    }
}
