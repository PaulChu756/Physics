using UnityEngine;
using System.Collections;

/* Unity has build in normailized and Mag. */

public class Particle : MonoBehaviour 
{
	public Vector3 position; // current pos of the particle in space
    public Vector3 nextPosition;
    public Vector3 velocity; // current velocity of the particle 
    public Vector3 nextVelocity;
    public Vector3 acceleration;
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
	
	// Didn't do Uniform Gravity yet, using rigidbody, but thinking about it.

    public void ParticleMath()
    {
        if(!isPinned)
        {
            acceleration = force / mass;
            nextVelocity = velocity + (acceleration * Time.deltaTime);
            nextPosition = position + (nextVelocity * Time.deltaTime);
            gameObject.transform.position += nextPosition;
        }
    }
}
