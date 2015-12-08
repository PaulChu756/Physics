using UnityEngine;
using System.Collections.Generic;

public class TeacherTest : MonoBehaviour
{
    public List<Spring> springs;
 
    public float k, b;
    // Update is called once per frame

    void Start()
    {
        springs = new List<Spring>();
        springs.AddRange(FindObjectsOfType<Spring>());
        springs[0].p1.isPinned = true;
        springs[1].p2.isPinned = true;
    }
    void Update()
    {

        foreach (Spring s in springs)
        {
            s.springConstant = k;
            s.dampingFactor = b;
        }

    }
}
