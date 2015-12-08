using UnityEngine;
using System.Collections;

/* Unity has build in normailized and Mag. */

public class Particle : MonoBehaviour
{
    public Vector3 position
    {
        get { return transform.position; }
        set { transform.position = value; }

    } // current pos of the particle in space
    public Vector3 nextPosition;
    public Vector3 velocity; // current velocity of the particle 
    public Vector3 nextVelocity;
    //  public Vector3 acceleration;
    public Vector3 force;
    public float mass;
    public bool isPinned;

    // r = pos
    // v = velocity
    // a = acceleration
    // m = mass
    // p = momentum
    // f = force
    // l = length;
    void Start()
    {
        position = transform.position;
    }
    public void ParticleMath()
    {
        if (!isPinned)
        {
            Vector3 acceleration = force / mass;
            velocity += (acceleration * Time.deltaTime);
            position += (velocity * Time.deltaTime);
        }
        

        //transform.position = position;

    }
}
