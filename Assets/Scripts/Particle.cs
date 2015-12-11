using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour
{
    public Vector3 position // current pos of the particle in space
    {
        get { return transform.position; }
        set { transform.position = value; } // Getting and Setting the position = transform.position
    } 
    public Vector3 velocity; // current velocity of the particle 
    public Vector3 force;
    public float mass;
    public bool isPinned;

    void Awake()
    {
        position = transform.position;
    }

    public void ParticleMath()
    {
        if (!isPinned)
        {
            Vector3 acceleration = force / mass; // We create acceleration right here, so it doesn't get reset
            velocity += (acceleration * Time.deltaTime);
            position += (velocity * Time.deltaTime);
        }
    }
}
