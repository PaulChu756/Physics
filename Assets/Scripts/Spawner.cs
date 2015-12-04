using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject SpringDamper;
    public GameObject P1;
    public GameObject P2;

    public List<GameObject> particleList = new List<GameObject>();
    public List<GameObject> springdamp = new List<GameObject>();

    public float k;
    public float d;
    public float b;
    public float stiff;

    public int rows = 5, cols = 5, width, length;

    void Awake()
    {
        SpawnParticles();
        //SpawnSpringDampers();
    }

    public void SpawnParticles()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject o = Instantiate(P1);
                o.transform.parent = gameObject.transform;
                o.transform.position = new Vector3(i * width / rows, j * length / cols, 0);
                particleList.Add(o);
            }
        }
    }



    public void ApplyGravity()
    {
        foreach(GameObject o in particleList)
        {
            Vector3 g = new Vector3(0, -9.8f, 0) * GetComponent<Particle>().mass;
            GetComponent<Particle>().force += g;
        }
    }
}


//public void SpawnSpringDampers()
//{
//    for(int i = 0; i < particleList.Count; i++)
//    {
//        if(i > width - 1)
//        {
//            if(particleList[i - width]) // Top
//            {
//                GameObject o = new GameObject("Spring");
//                o.transform.parent = gameObject.transform;
//                o.AddComponent<SpringDamper>();
//                MakeSpring(particleList[i], particleList[i - width], stiff, 175);
//                springdamp.Add(o);
//            }
//        }

//        if((i % width != width - 1) && i >= width) // Right
//        {
//            GameObject o = new GameObject("Spring");
//            o.transform.parent = gameObject.transform;
//            o.AddComponent<SpringDamper>();
//            MakeSpring(particleList[i], particleList[i - width], stiff,175);
//            springdamp.Add(o);
//        }

//        if (i > 0) // Left 
//        {
//            if (i % width != 0)
//            {
//                GameObject o = new GameObject("Spring");
//                o.transform.parent = gameObject.transform;
//                o.AddComponent<SpringDamper>();
//                MakeSpring(particleList[i], particleList[i - 1], stiff, 175);
//                springdamp.Add(o);
//            }
//        }
//    }
//}

//public float Distance(Vector3 a, Vector3 b)
//{
//    float x = b.x - a.x;
//    float y = b.y - a.x;
//    float z = b.z - a.z;

//    return Mathf.Sqrt(x * x + y * y + z * z);
//}
//public void MakeSpring(GameObject p1, GameObject p2, float stiff, float damper)
//{
//    P1 = p1;
//    P2 = p2;
//    k = stiff;
//    d = Distance(P1.transform.position, P2.transform.position);
//    b = damper;
//}

//public void Draw()
//{
//    Debug.DrawLine(P1.transform.position, P2.transform.position);
//}